using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library;
using Mono.Cecil;
using System.Diagnostics;
using MrKupido.Library.Recipe;
using System.Reflection;

namespace MrKupido.Processor.Model
{
    public class RecipeTreeNode : TreeNode
    {
        public delegate EquipmentGroup SelectEquipmentDelegate(float amount);
        public delegate PreparedIngredients PrepareDelegate(float amount, EquipmentGroup eg);
        public delegate CookedFoodParts CookDelegate(float amount, PreparedIngredients preps, EquipmentGroup eg);
        public delegate void ServeDelegate(float amount, CookedFoodParts food, EquipmentGroup eg);

        public DateTime? ExpiresAt;

        public SelectEquipmentDelegate SelectEquipment;
        public RecipeMethod SelectEquipmentMethod;
        public PrepareDelegate Prepare;
        public RecipeMethod PrepareMethod;
        public CookDelegate Cook;
        public RecipeMethod CookMethod;
        public ServeDelegate Serve;
        public RecipeMethod ServeMethod;

        public Type RecipeType { get; private set; }
        public Type RecipePatchedType { get; private set; }
        public RecipeBase StandardInstance { get; private set; }
        public object StandardPatchedInstance { get; private set; }

        public RecipeTreeNode(Type recipeType)
            : base(recipeType)
        {
            RecipeType = recipeType;
            StandardInstance = (RecipeBase)RecipeType.DefaultConstructor(1.0f);

            Assembly ass = Assembly.LoadFile(recipeType.Assembly.Location + ".Patched.dll");
            RecipePatchedType = ass.GetTypes().FirstOrDefault(t => t.FullName == recipeType.FullName);
            if (RecipePatchedType == null) throw new MrKupidoException("The type '{0}' was not found in the patched assembly '{1}'.", RecipeType.FullName, ass.Location);
            StandardPatchedInstance = RecipePatchedType.DefaultConstructor(1.0f);

            EquipmentGroup eg = null;
            PreparedIngredients preps = null;
            CookedFoodParts cfp = null;

            AssemblyDefinition ad = AssemblyDefinition.ReadAssembly(recipeType.Assembly.Location);
            TypeDefinition td = ad.MainModule.Types.Where(t => t.FullName == recipeType.FullName).FirstOrDefault();
            if (td == null) throw new MrKupidoException("Mono.Cecil can't read the type definition of '{0}' in assembly '{1}'.", recipeType.FullName, recipeType.Assembly.Location);

            try
            {
                SelectEquipmentMethod = new RecipeMethod(RecipeType, RecipePatchedType, td, "SelectEquipment");
                eg = StandardInstance.SelectEquipment(1.0f); // we need this as well, otherwise we can't get the delegate working
                SelectEquipment = (SelectEquipmentDelegate)SelectEquipmentMethod.PatchedDM.CreateDelegate(typeof(SelectEquipmentDelegate));
            }
            catch (Exception) { }

            try
            {
                PrepareMethod = new RecipeMethod(RecipeType, RecipePatchedType, td, "Prepare");
                preps = (PreparedIngredients)StandardInstance.Prepare(1.0f, eg); // we need this as well, otherwise we can't get the delegate working
                Prepare = (PrepareDelegate)PrepareMethod.PatchedDM.CreateDelegate(typeof(PrepareDelegate));
            }
            catch (Exception) { }

            try
            {
                CookMethod = new RecipeMethod(RecipeType, RecipePatchedType, td, "Cook");
                cfp = (CookedFoodParts)StandardInstance.Cook(1.0f, preps, eg); // we need this as well, otherwise we can't get the delegate working
                Cook = (CookDelegate)CookMethod.PatchedDM.CreateDelegate(typeof(CookDelegate));
            }
            catch (Exception) {}

            try
            {
                ServeMethod = new RecipeMethod(RecipeType, RecipePatchedType, td, "Serve");
                StandardInstance.Serve(1.0f, cfp, eg); // we need this as well, otherwise we can't get the delegate working
                Serve = (ServeDelegate)ServeMethod.PatchedDM.CreateDelegate(typeof(ServeDelegate));
            }
            catch (Exception) { }

        }
    }
}
