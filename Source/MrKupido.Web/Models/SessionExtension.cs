using MrKupido.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace MrKupido.Web.Models
{
    public static class SessionExtension
    {
        public static User GetCurrentUser(this HttpSessionStateBase session)
        {
            if (session["CurrentUser"] == null)
            {
                HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                if (authCookie != null)
                {
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                    session["CurrentUser"] = new User().FromJSONString(ticket.UserData);
                }
            }


            return (MrKupido.Model.User)session["CurrentUser"];
        }

        public static void SetCurrentUser(this HttpSessionStateBase session, User user)
        {
            session["CurrentUser"] = user;
        }

        public static string GetCurrentUserDisplayName(this HttpSessionStateBase session)
        {
            User user = GetCurrentUser(session);

            if (user == null) return "Anonymous";

            return (!String.IsNullOrEmpty(user.NickName) ? user.NickName : user.FullName);
        }

        public static string GetCurrentUserAvatarUrl(this HttpSessionStateBase session, bool displayAvatar)
        {
            User user = GetCurrentUser(session);
            isAvatarCached |= displayAvatar;
            return ((user != null) && !String.IsNullOrEmpty(user.AvatarUrl) ? user.AvatarUrl : "Content/svg/icon_avatar.svg");
        }        

        private static bool isAvatarCached = false;
        public static bool IsAvatarCached(this HttpSessionStateBase session)
        {
            return isAvatarCached;
        }
    }
}