using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mono.Cecil;
using System.Diagnostics;
using System.Reflection;
using MrKupido.Library;
using MrKupido.Library.Recipe;
using System.Web.Script.Serialization;
using MrKupido.Library.Attributes;
using MrKupido.Library.Ingredient;

namespace MrKupido.Processor.Model
{
	public class RecipeTreeNode : TreeNode
	{
		public delegate EquipmentGroup SelectEquipmentDelegate(float amount);
		public delegate PreparedIngredients PrepareDelegate(float amount, EquipmentGroup eg);
		public delegate CookedFoodParts CookDelegate(float amount, PreparedIngredients preps, EquipmentGroup eg);
		public delegate void ServeDelegate(float amount, CookedFoodParts food, EquipmentGroup eg);

		[ScriptIgnore]
		public DateTime? ExpiresAt;

		public string Version { get; protected set; }

		[ScriptIgnore]
		public SelectEquipmentDelegate SelectEquipment;
		[ScriptIgnore]
		public PrepareDelegate Prepare;
		[ScriptIgnore]
		public CookDelegate Cook;
		[ScriptIgnore]
		public ServeDelegate Serve;

		[ScriptIgnore]
		private Dictionary<float, IIngredient[]> ingredientCache = new Dictionary<float, IIngredient[]>();
		[ScriptIgnore]
		private Dictionary<float, IDirection[]> directionCache = new Dictionary<float, IDirection[]>();

		[ScriptIgnore]
		public Type RecipeType { get; private set; }

		[ScriptIgnore]
		public string[] SearchStrings { get; private set; }

		public ShoppingListCategory MainCategory;

		public bool IsImplemented = false;
		public bool IsAbtract = false;
		public bool IsInline = false;
		public bool IsIngrec = false;

		public string ManTags = "";

		public float TotalWeight;
		public float? TotalCalories = null;
		public float TotalCaloriesCompletion;
		public float? TotalCarbohydrates = null;
		public float TotalCarbohydratesCompletion;
		public float? TotalProtein = null;
		public float TotalProteinCompletion;
		public float? TotalFat = null;
		public float TotalFatCompletion;

		public float StandardPortionCalories = 1000.0f;
		public float PortionMultiplier = 1.00f;

		public CommercialProductAttribute CommercialAttribute = null;

		public RecipeTreeNode(Type recipeType, string languageISO)
			: base(recipeType, languageISO)
		{
			int bracketStart = LongName.IndexOf('[');
			int bracketEnd = bracketStart >= 0 ? LongName.IndexOf(']', bracketStart) : -1;

			Version = bracketStart == -1 ? "" : LongName.Substring(bracketStart + 1, bracketEnd - bracketStart - 1);
			LongName = bracketStart == -1 ? LongName : LongName.Substring(0, bracketStart);

			char[] name = LongName.ToCharArray();
			name[0] = Char.ToUpper(name[0]);
			LongName = new string(name);

			RecipeType = recipeType;

			MethodInfo mi = RecipeType.GetMethod("SelectEquipment");
			if (mi != null)
			{
				SelectEquipment = (SelectEquipmentDelegate)Delegate.CreateDelegate(typeof(SelectEquipmentDelegate), mi);
			}

			mi = RecipeType.GetMethod("Prepare");
			if (mi != null)
			{
				Prepare = (PrepareDelegate)Delegate.CreateDelegate(typeof(PrepareDelegate), mi);
				IsImplemented = true;
			}

			mi = RecipeType.GetMethod("Cook");
			if (mi != null)
			{
				Cook = (CookDelegate)Delegate.CreateDelegate(typeof(CookDelegate), mi);
				IsImplemented = true;
			}

			mi = RecipeType.GetMethod("Serve");
			if (mi != null)
			{
				Serve = (ServeDelegate)Delegate.CreateDelegate(typeof(ServeDelegate), mi);
			}

			IngredientConstsAttribute[] ica = (IngredientConstsAttribute[])recipeType.GetCustomAttributes(typeof(IngredientConstsAttribute), false);
			if (ica.Length > 0)
			{
				IsAbtract = ica[0].IsAbstract;
				IsInline = ica[0].IsInline && IsImplemented;  // if it's not implemented, we should not handle it as inline
				IsIngrec = ica[0].IsIngrec;

				ManTags = String.IsNullOrEmpty(ica[0].ManTags) ? "" : ica[0].ManTags;

				if (ica[0].StandardPortionCalories != Single.MinValue)
				{
					StandardPortionCalories = ica[0].StandardPortionCalories;
				}
			}

			CommercialProductAttribute[] commercialAttributes = (CommercialProductAttribute[])recipeType.GetCustomAttributes(typeof(CommercialProductAttribute), false);
			if (commercialAttributes.Length > 0)
			{
				CommercialAttribute = commercialAttributes[0];
			}

			SearchStrings = new string[0];
		}

		public RuntimeIngredient[] GetIngredients(float amount, int multiplier)
		{
			List<RuntimeIngredient> result = new List<RuntimeIngredient>();

			IIngredient[] ingredients = GetRecipeIngredients(amount, multiplier);

			foreach (IngredientBase i in ingredients)
			{
				string classFullName = i.GetType().FullName;

				if (i is RecipeBase)
				{
					RecipeTreeNode rtn = Cache.Recipe[classFullName];
					if (rtn != null)
					{
						result.Add(new RuntimeIngredient(i, rtn));
						continue;
					}
				}
				else
				{
					IngredientTreeNode itn = Cache.Ingredient[classFullName];
					if (itn != null)
					{
						result.Add(new RuntimeIngredient(i, itn));
						continue;
					}
				}

				throw new MrKupidoException("Ingredient '{0}' was not found eighter in the ingredients or recipes trees.");
			}


			float completionStep = ingredients.Length > 0 ? 1.0f / ingredients.Length : 0.0f;

			foreach (RuntimeIngredient i in result)
			{
				if ((this.MainCategory == ShoppingListCategory.Unknown) && (i.Ingredient is SingleIngredient) && (((SingleIngredient)i.Ingredient).Category.HasValue))
				{
					this.MainCategory = ((SingleIngredient)i.Ingredient).Category.Value;
				}

				float ingredientGramms = i.Ingredient.GetAmount(MeasurementUnit.gramm);
				this.TotalWeight += ingredientGramms;

				if (!i.Ingredient.CaloriesPer100Gramms.HasValue)
				{
					Trace.TraceWarning("Ingredient '{0}' doesn't have calories defined.", i.Ingredient.Name);
				}

				AddOrSet(ref this.TotalCalories, i.Ingredient.CaloriesPer100Gramms * ingredientGramms / 100, ref this.TotalCaloriesCompletion, completionStep);
				AddOrSet(ref this.TotalCarbohydrates, i.Ingredient.CarbohydratesPer100Gramms * ingredientGramms / 100, ref this.TotalCarbohydratesCompletion, completionStep);
				AddOrSet(ref this.TotalProtein, i.Ingredient.ProteinPer100Gramms * ingredientGramms / 100, ref this.TotalProteinCompletion, completionStep);
				AddOrSet(ref this.TotalFat, i.Ingredient.FatPer100Gramms * ingredientGramms / 100, ref this.TotalFatCompletion, completionStep);
			}
			if (this.TotalCaloriesCompletion > 1.00) this.TotalCaloriesCompletion = 1.00f;
			if (this.TotalCarbohydratesCompletion > 1.00) this.TotalCarbohydratesCompletion = 1.00f;
			if (this.TotalProteinCompletion > 1.00) this.TotalProteinCompletion = 1.00f;
			if (this.TotalFatCompletion > 1.00) this.TotalFatCompletion = 1.00f;

			if ((this.PortionMultiplier == 1.0))// && (amount == 1.0f))
			{
				if (this.TotalCaloriesCompletion == 1.0)
				{
					this.PortionMultiplier = this.StandardPortionCalories / this.TotalCalories.Value / amount;
				}
				else
				{
					// let's suppose that the original recipe is 4000 calories
					this.PortionMultiplier = this.StandardPortionCalories / 4000.0f / amount;
				}
			}

			if ((this.PortionMultiplier == 1.0) && (this.TotalCaloriesCompletion > 0.0) && (this.TotalCaloriesCompletion < 1.0))
			{
				Trace.TraceWarning("Recipe '{0}' does not have all the ingredients' calories information, so the portion multiplier can not be calculated.", this.UniqueName);
			}

			return result.ToArray();
		}

		public TagTreeNode[] GetTags()
		{
			List<TagTreeNode> result = new List<TagTreeNode>();

			foreach (TagTreeNode tag in Cache.Tag.Indexer.All)
			{
				ITag tagObject = (ITag)Activator.CreateInstance(tag.ClassType);
				if (tagObject.IsMatch(this)) result.Add(tag);
			}

			return result.ToArray();
		}

		public void BuildSearchStrings()
		{
			List<string> searchStrings = new List<string>();

			// --- Ingredients
			IIngredient[] ingredients = this.GetRecipeIngredients(1.0f, 1);

			// add this class and its parent
			searchStrings.Add(this.NodeType + ":" + this.UniqueName);

			TreeNode currentParent = this.Parent;
			while (currentParent != null)
			{
				searchStrings.Add(currentParent.NodeType + ":" + currentParent.UniqueName);
				currentParent = currentParent.Parent;
			}

			// add all ingredients and all of their parents to the ingredientsForSearch collection
			foreach (IngredientBase ib in ingredients)
			{
				IngredientBase currentIb = ib;
				string ibTypeFullName = ib.GetType().FullName;

				TreeNode itn = Cache.Ingredient.All.FirstOrDefault(tn => tn.ClassType.FullName == ibTypeFullName);
				if (itn == null) itn = Cache.Recipe.All.FirstOrDefault(tn => tn.ClassType.FullName == ibTypeFullName);

				while (itn != null)
				{
					searchStrings.Add(itn.NodeType + ":" + itn.UniqueName);

					itn = itn.Parent;
				}
			}

			// if this is a commercial product, let's inherit its parent's ingredientlist
			if ((this.CommercialAttribute != null) && (this.Parent != null) && (this.Parent is RecipeTreeNode))
			{
				searchStrings.AddRange(((RecipeTreeNode)this.Parent).SearchStrings);
			}



			// --- Tags

			foreach (TagTreeNode tag in this.GetTags())
			{
				searchStrings.Add(tag.NodeType + ":" + tag.UniqueName);
			}


			this.SearchStrings = searchStrings.ToArray();
		}

		private IIngredient[] GetRecipeIngredients(float amount, int multiplier)
		{
			float am = amount * this.PortionMultiplier * multiplier;

			lock (ingredientCache)
			{
				if (!ingredientCache.ContainsKey(am))
				{
					ingredientCache.Add(am, RecipeAnalyzer.GenerateIngredients(this, am));
				}
			}

			return ingredientCache[am];
		}

		public IEquipment[] GetEquipments(float amount, int multiplier)
		{
			return new MrKupido.Library.Equipment.EquipmentBase[0];
		}

		public IDirection[] GetDirections(float amount, int multiplier)
		{
			float am = amount * this.PortionMultiplier * multiplier;

			if (!directionCache.ContainsKey(am))
			{
				directionCache.Add(am, RecipeAnalyzer.GenerateDirections(this, am));
			}

			return directionCache[am];
		}

		public INutritionInfo[] GetNutritions(float amount, int multiplier)
		{
			return new NutritionInfo[0];
		}
	}
}
