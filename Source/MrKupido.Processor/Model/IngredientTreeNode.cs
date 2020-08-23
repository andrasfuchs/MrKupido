using MrKupido.Library.Attributes;
using System;
using System.Diagnostics;

namespace MrKupido.Processor.Model
{
    public class IngredientTreeNode : TreeNode
    {
        private NatureRelationAttribute taxonomyConnectionAttribute = null;
        public Type NatureConnectionClassType { set; get; }

        public IngredientTreeNode(Type ingredientClass, string languageISO)
            : base(ingredientClass, languageISO)
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

            if ((!hasConnectionToTaxonomyTree) && (!ingredientClass.IsAbstract)) Trace.TraceWarning("Class '{0}' has no connection to the taxonomy tree.", ingredientClass.Name);

            ShortName = LongName;
            if (hasConnectionToTaxonomyTree && (taxonomyConnectionAttribute != null))
            {
                LongName += " [" + NameAliasAttribute.GetName(this.LanguageISO, taxonomyConnectionAttribute.NatureClass) + " " + NameAliasAttribute.GetName(this.LanguageISO, taxonomyConnectionAttribute.GetType()) + "]";
            }


        }
    }
}

