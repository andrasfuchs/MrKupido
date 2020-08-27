using System;

namespace MrKupido.Web.Core.Models
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