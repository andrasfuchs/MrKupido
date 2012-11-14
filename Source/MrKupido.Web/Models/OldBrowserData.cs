using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MrKupido.Web.Models
{
    [Serializable]
    public class OldBrowserData
    {
        public string BrowserName;
        public string BrowserVersion;

        public string UpdateUrl;
        public string ReturnUrl;
    }
}