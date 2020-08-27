using Microsoft.AspNetCore.Http;
using MrKupido.Model;
using System;
using System.Web;
using System.Web.Security;

namespace MrKupido.Web.Core.Models
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
            return ((user != null) && (user.AvatarUrl != null) ? user.AvatarUrl.ToString() : "Content/svg/icon_avatar.svg");
        }

        public static string GetCurrentLanguageString(this HttpSessionStateBase session)
        {
            string propertyName = System.Threading.Thread.CurrentThread.CurrentUICulture.ThreeLetterISOLanguageName;
            propertyName = "Language" + Char.ToUpper(propertyName[0]) + propertyName.Substring(1);
            return typeof(Resources.Shared.SearchHeader).GetProperty(propertyName).GetValue(null) as string;
        }

        private static bool isAvatarCached = false;
        public static bool IsAvatarCached(this HttpSessionStateBase session)
        {
            return isAvatarCached;
        }
    }
}