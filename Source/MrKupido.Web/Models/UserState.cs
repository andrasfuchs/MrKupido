using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MrKupido.Model;
using System.Web.Routing;
using System.Text;

namespace MrKupido.Web.Models
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

        private RequestContext lastRequest;
        public RequestContext RequestContext
        {
            set
            {
                StringBuilder sb = new StringBuilder();
                foreach (string key in value.HttpContext.Request.Params.AllKeys)
                {
                    if ((key == "ASP.NET_SessionId") || (key.ToUpper() == key) || key.StartsWith("__")) continue;
                    sb.Append(key);
                    sb.Append(':');
                    sb.Append(value.HttpContext.Request.Params[key]);
                    sb.Append(',');
                }

                OnChanged("URL:" + value.HttpContext.Request.Url, sb.ToString());

                lastRequest = value;
                LastActionUTC = DateTime.UtcNow;
            }
        }
    }
}