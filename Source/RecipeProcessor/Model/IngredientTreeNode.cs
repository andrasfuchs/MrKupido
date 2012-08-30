using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using MrKupido.Library.Attributes;

namespace MrKupido.Processor.Model
{
    public class IngredientTreeNode : TreeNode
    {
        private NatureRelationAttribute taxonomyConnectionAttribute = null;
        public Type NatureConnectionClassType { set; get; }

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
        }
    }
}

