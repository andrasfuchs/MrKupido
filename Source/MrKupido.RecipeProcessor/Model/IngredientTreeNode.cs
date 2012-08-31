using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Linq;
using MrKupido.Library.Attributes;
using MrKupido.Library.Ingredient;
using MrKupido.DataAccess;
using MrKupido.Model;
using MrKupido.Library;

namespace MrKupido.Processor.Model
{
    public class IngredientTreeNode : TreeNode
    {
        private NatureRelationAttribute taxonomyConnectionAttribute = null;
        public Type NatureConnectionClassType { set; get; }
        public IngredientBase StandardInstance { get; private set; }

        public IngredientTreeNode(Type ingredientClass)
            : base(ingredientClass)
        {
            bool hasConnectionToTaxonomyTree = false;

            foreach (object attr in ingredientClass.GetCustomAttributes(true))
            {
                if (attr.GetType().BaseType == typeof(NatureRelationAttribute))
                {
                    hasConnectionToTaxonomyTree = true;
                    taxonomyConnectionAttribute = (NatureRelationAttribute)attr;
                }
            }

            if (!hasConnectionToTaxonomyTree) Trace.TraceWarning("Class '{0}' has no connrection to the taxonomy tree.", ingredientClass.Name);

            if (hasConnectionToTaxonomyTree && (taxonomyConnectionAttribute != null))
            {
                Name += " [" + NameAliasAttribute.GetDefaultName(taxonomyConnectionAttribute.NatureClass) + " " + NameAliasAttribute.GetDefaultName(taxonomyConnectionAttribute.GetType()) + "]";
            }

            Ingredient dbIngredient = db.Ingredients.Where(i => i.ClassName == ingredientClass.Name).FirstOrDefault();

            this.StandardInstance = ingredientClass.DefaultConstructor(1.0f, MeasurementUnit.gramm, IngredientState.Normal) as IngredientBase;

            if (this.StandardInstance != null)
            {
                this.StandardInstance.LoadStaticInfoObject(dbIngredient);
            }
        }
    }
}

