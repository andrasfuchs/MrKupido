using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Attributes
{
    [AttributeUsage(System.AttributeTargets.Class, AllowMultiple = false)]
    public class CommercialProductAttribute : Attribute
    {
        public Type Brand;
        public Type MadeBy;
        public Type DistributedBy;
        public string BarCode;

        public CommercialProductAttribute()
        {
        }

        public static string GetShortName(Type objType, string languageISOCode)
        {
            foreach (object attr in objType.GetCustomAttributes(typeof(CommercialProductAttribute), false))
            {
                CommercialProductAttribute commercialAttribute = (CommercialProductAttribute)attr;
                ProviderBase pb = null;

                if (commercialAttribute.Brand != null)
                {
                    pb = (ProviderBase)Activator.CreateInstance(commercialAttribute.Brand, languageISOCode);
                    return pb.BrandName;
                }

                if (commercialAttribute.MadeBy != null)
                {
                    pb = (ProviderBase)Activator.CreateInstance(commercialAttribute.MadeBy, languageISOCode);
                    return pb.CompanyName;
                }

                if (commercialAttribute.DistributedBy != null)
                {
                    pb = (ProviderBase)Activator.CreateInstance(commercialAttribute.DistributedBy, languageISOCode);
                    return pb.CompanyName;
                }
            }

            return null;
        }
    }
}
