//-----------------------------------------------------------------------
// <copyright file="FacebookGraph.cs" company="Outercurve Foundation">
//     Copyright (c) Outercurve Foundation. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace DotNetOpenAuth.ApplicationBlock.Facebook {
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Json;
    using System.Text;

    [DataContract]
    public class FacebookGraph {
        private static DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(FacebookGraph));

        [DataMember(Name = "id")]
        public long Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "first_name")]
        public string FirstName { get; set; }

        [DataMember(Name = "last_name")]
        public string LastName { get; set; }

        [DataMember(Name = "link")]
        public Uri Link { get; set; }

        [DataMember(Name = "birthday")]
        public string Birthday { get; set; }

        [DataMember(Name = "birthday_date")]
        public string BirthdayDate { get; set; }

        [DataMember(Name = "gender")]
        public string Gender { get; set; }

        [DataMember(Name = "locale")]
        public string Locale { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "picture")]
        public FacebookPicture Picture { get; set; }

        public static FacebookGraph Deserialize(string json) {
            if (string.IsNullOrEmpty(json)) {
                throw new ArgumentNullException("json");
            }

            return Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(json)));
        }

        public static FacebookGraph Deserialize(Stream jsonStream) {
            if (jsonStream == null) {
                throw new ArgumentNullException("jsonStream");
            }

            return (FacebookGraph)jsonSerializer.ReadObject(jsonStream);
        }

        public struct FacebookPicture
        {
            [DataMember(Name = "data")]
            public FacebookPictureData Data { get; set; }
        }

        public struct FacebookPictureData
        {
            [DataMember(Name = "url")]
            public string Url { get; set; }

            [DataMember(Name = "is_silhouette")]
            public string Is_Silhouette { get; set; }
        }
    }
}
