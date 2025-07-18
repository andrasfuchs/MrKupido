using MrKupido.Model;
using System;

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
    }
}