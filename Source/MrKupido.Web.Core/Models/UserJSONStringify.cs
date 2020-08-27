using MrKupido.Model;
using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace MrKupido.Web.Core.Models
{
    public static class UserJSONStringify
    {
        private static DataContractJsonSerializer dcjs = new DataContractJsonSerializer(typeof(User));

        public static string ToJSONString(this User user)
        {
            MemoryStream ms = new MemoryStream();
            dcjs.WriteObject(ms, user);
            return Encoding.UTF8.GetString(ms.ToArray());
        }

        public static User FromJSONString(this User user, string str)
        {
            if (String.IsNullOrEmpty(str)) return null;

            return dcjs.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(str))) as User;
        }
    }
}