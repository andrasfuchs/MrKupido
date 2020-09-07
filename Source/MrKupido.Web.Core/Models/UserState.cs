using Microsoft.AspNetCore.Http;
using MrKupido.Model;
using System;
using System.Text;

namespace MrKupido.Web.Core.Models
{
    public delegate void UserStateChangedEventHandler(object sender, DateTime utc, string ip, string sessionId, string action, string parameters);

    public class UserState
    {
        public event UserStateChangedEventHandler Changed;

        // Invoke the Changed event; called whenever list changes
        protected virtual void OnChanged(string action, string parameters)
        {
            if (Changed != null)
                Changed(this, DateTime.UtcNow, IPAddress, SessionID, action, parameters);
        }

        public string SessionID;
        public string IPAddress;
        public DateTime LastActionUTC;

        private User user;
        public User User
        {
            set
            {
                if (value == null)
                {
                    OnChanged("LOGOUT", null);
                }
                else
                {
                    if ((user == null) || (value.UserId != user.UserId))
                    {
                        user = value;
                        LastActionUTC = DateTime.UtcNow;

                        OnChanged("LOGIN", value.UserId.ToString());
                    }
                }
            }

            get
            {
                return user;
            }
        }

        private HttpContext lastRequest;
        public HttpContext RequestContext
        {
            set
            {
                StringBuilder sb = new StringBuilder();
                foreach (string key in value.Request.Query.Keys)
                {
                    if ((key == "ASP.NET_SessionId") || (key.ToUpper() == key) || key.StartsWith("__") || (key.StartsWith("DotNetOpenAuth.WebServerClient.XSRF-Session"))) continue;
                    sb.Append(key);
                    sb.Append(':');
                    sb.Append(value.Request.Query[key]);
                    sb.Append(',');
                }

                OnChanged("URL:" + value.Request.Path, sb.ToString());

                lastRequest = value;
                LastActionUTC = DateTime.UtcNow;
            }

            get
            {
                return lastRequest;
            }
        }
    }
}