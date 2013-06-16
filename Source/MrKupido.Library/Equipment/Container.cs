using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Ingredient;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "container")]
    [NameAlias("hun", "tároló")]
    public abstract class Container : EquipmentBase, IIngredientContainer
    {
        public Dimensions Dimensions { get; protected set; }

        public int Id { get; set; }

        protected IngredientGroup contents = new IngredientGroup();

		[NameAlias("eng", "contents of the {}")]
        [NameAlias("hun", "{} tartalma")]
        public IIngredient Contents 
        {
            get
            {
                return getContents();
            }
        }

        public Container(float width, float height, float depth)
        {
            this.Dimensions = new Dimensions(width, height, depth);
        }

        protected IIngredient getContents()
        {
                if (contents.IngredientCount == 0)
                {
                    return null;
                }
                else if (contents.IngredientCount == 1)
                {
                    return contents.Ingredients[0];
                }
                else
                {
                    return contents;
                }
        }

        [NameAlias("eng", "wait", Priority = 200)]
        [NameAlias("hun", "vár", Priority = 200)]
		[NameAlias("eng", "wait {0}")]
        [NameAlias("hun", "várj {0T}")]
        [PassiveAction]
        public void Varni(Quantity duration) 
        {
            this.LastActionDuration = (uint)(duration.GetAmount(MeasurementUnit.minute) * 60);
        }

        [NameAlias("eng", "pour off", Priority = 200)]
        [NameAlias("hun", "leönt", Priority = 200)]
		[NameAlias("eng", "pour off the fluid from the {}")]
        [NameAlias("hun", "öntsd le a folyadékot a(z) {L}")]
        public void FolyadekotLeonteni() 
        {
            List<IIngredient> newContent = new List<IIngredient>();

            foreach (ISingleIngredient i in contents.Ingredients)
            {
                if (!i.IsFluid) newContent.Add(i);
            }

            this.LastActionDuration = 120;

            this.Empty();
            this.AddRange(newContent.ToArray());
        }

        [NameAlias("eng", "pour off", Priority = 200)]
        [NameAlias("hun", "átönt", Priority = 200)]
		[NameAlias("eng", "pour off the fluid from the {} into the {0}")]
        [NameAlias("hun", "öntsd át a folyadékot a(z) {L} a(z) {0B}")]
        public void FolyadekotAtonteni(IIngredientContainer c)
        {
            List<IIngredient> newContent = new List<IIngredient>();
            List<IIngredient> liquids = new List<IIngredient>();

            foreach (IIngredient i in contents.Ingredients)
            {
                if (!i.IsFluid)
                {
                    newContent.Add(i);
                }
                else 
                {
                    liquids.Add(i);
                }
            }

            this.LastActionDuration = 120;

            this.Empty();
            this.AddRange(newContent.ToArray());

            c.AddRange(liquids.ToArray());
        }


		[NameAlias("eng", "separate", Priority = 200)]
		[NameAlias("hun", "szétválaszt", Priority = 200)]
		[NameAlias("eng", "separate the {.Contents.} into the {0} and {1} in a way, that the first has {2}% of it")]
		[NameAlias("hun", "válaszd szét a(z) {.Contents.T} a(z) {0B} és a(z) {1B}, úgy hogy az elsőbe kb. {2}% kerüljön")]
		public void Szetvalasztani(Container c1, Container c2, float p)
		{
			if ((p < 0.0f) || (p > 100.0f)) throw new MrKupidoException("Invalid percentage value was passed. It must be between 0.0 and 1.0.");

			Container oc = (Container)this.Clone();
			this.Empty();

			//c1.Add(oc.Contents * p / 100.0f);
			//c2.Add(oc.Contents * (100.0f - p) / 100.0f);

			c1.Add(oc.Contents);
			c2.Add(oc.Contents);

			this.LastActionDuration = 30;
		}


		[NameAlias("eng", "sprinkle", Priority = 200)]
		[NameAlias("hun", "rászór", Priority = 200)]
		[NameAlias("eng", "sprinkle onto the {} the following: ({0*}, )")]
		[NameAlias("hun", "szórd rá a(z) {R} a következőket: ({0*}, )")]
		public void Raszorni(params IIngredient[] ingredients)
		{
			this.contents.AddIngredients(ingredients);
			this.LastActionDuration = 60 * (uint)ingredients.Length;
		}



        [NameAlias("eng", "add", Priority = 200)]
        [NameAlias("hun", "hozzáad", Priority = 200)]
        public void Add(IIngredient i)
        {
            this.contents.AddIngredients(i);
        }

        [NameAlias("eng", "add more", Priority = 200)]
        [NameAlias("hun", "többeket hozzáad", Priority = 200)]
        public void AddRange(IIngredient[] i)
        {
            this.contents.AddIngredients(i);
        }

        [NameAlias("eng", "empty", Priority = 200)]
        [NameAlias("hun", "kiürít", Priority = 200)]
        public void Empty()
        {
            this.contents = new IngredientGroup();
        }

		public override object Clone()
		{
			object result = this.MemberwiseClone();
			((Container)result).contents = new IngredientGroup(this.contents);

			return result;
		}
    }
}
