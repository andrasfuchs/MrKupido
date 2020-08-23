using System;

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