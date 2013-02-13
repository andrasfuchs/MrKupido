using MrKupido.Library.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library
{
    public class NamedObject : MarshalByRefObject
    {
        private Dictionary<string ,string> names = new Dictionary<string,string>();

        [Obsolete("This must be used only internally for debugging")]
        public string Name
        {
            get
            {
                return GetName(System.Threading.Thread.CurrentThread.CurrentUICulture.ThreeLetterISOLanguageName);
            }
        }

        public virtual string GetName(string languageISO)
        {
            if (!names.ContainsKey(languageISO))
            {
                names.Add(languageISO, NameAliasAttribute.GetName(languageISO, this.GetType()));
            }

            return names[languageISO];
        }

        [Obsolete("This must be used only internally for debugging")]
        public override string ToString()
        {
            return GetName(System.Threading.Thread.CurrentThread.CurrentUICulture.ThreeLetterISOLanguageName);
        }

    }
}
