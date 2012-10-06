using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Attributes
{
    public class IconUriFragmentAttribute : Attribute
    {
        public string UriFragment { get; private set; }

        public IconUriFragmentAttribute(string uriFragment)
        {
            this.UriFragment = uriFragment;
        }
    }
}
