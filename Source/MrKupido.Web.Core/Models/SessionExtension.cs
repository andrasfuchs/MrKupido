using Microsoft.AspNetCore.Http;
using MrKupido.Model;
using System;
using System.Text.Json;

namespace MrKupido.Web.Core.Models
{
    public static class SessionExtension
    {
        public static User GetCurrentUser(this ISession session)
        {
            if (session.GetCurrentUser() == null)
            {
                HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                if (authCookie != null)
                {
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                    session.SetCurrentUser(new User().FromJSONString(ticket.UserData));
                }
            }


            return session.GetObject<MrKupido.Model.User>("CurrentUser");
        }

        public static void SetCurrentUser(this ISession session, MrKupido.Model.User user)
        {
            session.SetObject("CurrentUser", user);
        }

        public static T GetObject<T>(this ISession session, string key)
        {
            string objectJson = session.GetString(key);

            if (String.IsNullOrEmpty(objectJson))
            {
                return default(T);
            }

            return JsonSerializer.Deserialize<T>(objectJson);
        }

        public static void SetObject<T>(this ISession session, string key, T value)
        {
            string objectJson = JsonSerializer.Serialize(value);

            session.SetString(key, objectJson);
        }

        public static string GetCurrentUserDisplayName(this ISession session)
        {
            User user = GetCurrentUser(session);

            if (user == null) return "Anonymous";

            return (!String.IsNullOrEmpty(user.NickName) ? user.NickName : user.FullName);
        }

        public static string GetCurrentUserAvatarUrl(this ISession session, bool displayAvatar)
        {
            User user = GetCurrentUser(session);
            isAvatarCached |= displayAvatar;
            return ((user != null) && (user.AvatarUrl != null) ? user.AvatarUrl.ToString() : "Content/svg/icon_avatar.svg");
        }

        public static string GetCurrentLanguageString(this ISession session)
        {
            string propertyName = System.Threading.Thread.CurrentThread.CurrentUICulture.ThreeLetterISOLanguageName;
            propertyName = "Language" + Char.ToUpper(propertyName[0]) + propertyName.Substring(1);
            return typeof(Resources.Shared.SearchHeader).GetProperty(propertyName).GetValue(null) as string;
        }

        private static bool isAvatarCached = false;
        public static bool IsAvatarCached(this ISession session)
        {
            return isAvatarCached;
        }
    }
}