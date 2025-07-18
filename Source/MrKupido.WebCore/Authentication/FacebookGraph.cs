using System;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace MrKupido.WebCore.Authentication
{
    [DataContract]
    public class FacebookGraph : IOAuth2Graph
    {
        private static DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(FacebookGraph));

        [DataMember(Name = "id")]
        public string Id { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "first_name")]
        public string FirstName { get; set; }
        [DataMember(Name = "middle_name")]
        public string MiddleName { get; set; }
        [DataMember(Name = "last_name")]
        public string LastName { get; set; }
        [DataMember(Name = "gender")]
        public string Gender { get; set; }
        [DataMember(Name = "locale")]
        public string Locale { get; set; }
        [DataMember(Name = "languages")]
        public FacebookIdName[] Languages { get; set; }
        [DataMember(Name = "link")]
        public Uri Link { get; set; }
        [DataMember(Name = "username")]
        public string Username { get; set; }
        [DataMember(Name = "timezone")]
        public int? Timezone { get; set; }
        [DataMember(Name = "updated_time")]
        public string UpdatedTime { get; set; }
        [DataMember(Name = "birthday")]
        public string Birthday { get; set; }
        [Obsolete]
        [DataMember(Name = "birthday_date")]
        public string BirthdayDate { get; set; }
        [DataMember(Name = "email")]
        public string Email { get; set; }
        [DataMember(Name = "hometown")]
        public FacebookIdName Hometown { get; set; }
        [DataMember(Name = "interested_in")]
        public string[] InterestedIn { get; set; }
        [DataMember(Name = "location")]
        public FacebookIdName Location { get; set; }
        [DataMember(Name = "political")]
        public string Political { get; set; }
        [Obsolete]
        [DataMember(Name = "favorite_athletes")]
        public FacebookIdName[] FavoriteAthletes { get; set; }
        [Obsolete]
        [DataMember(Name = "favorite_teams")]
        public FacebookIdName[] FavoriteTeams { get; set; }
        [DataMember(Name = "picture")]
        public FacebookPicture Picture { get; set; }
        [DataMember(Name = "quotes")]
        public Uri Quotes { get; set; }
        [DataMember(Name = "relationship_status")]
        public string RelationshipStatus { get; set; }
        [DataMember(Name = "religion")]
        public string Religion { get; set; }
        [DataMember(Name = "significant_other")]
        public FacebookIdName SignificantOther { get; set; }
        [DataMember(Name = "website")]
        public Uri Website { get; set; }

        public DateTime? BirthdayDT
        {
            get
            {
                if (!String.IsNullOrEmpty(this.Birthday) && (this.Locale != null))
                {
                    CultureInfo ci = new CultureInfo(this.Locale.Replace('_', '-'));
                    return DateTime.Parse(this.Birthday, ci);
                }
                return null;
            }
        }

        public Uri AvatarUrl
        {
            get
            {
                if ((this.Picture != null) && (this.Picture.Data != null) && (this.Picture.Data.Url != null))
                {
                    return this.Picture.Data.Url;
                }
                return null;
            }
        }

        public HumanGender GenderEnum
        {
            get
            {
                if (this.Gender == "male") return HumanGender.Male;
                else if (this.Gender == "female") return HumanGender.Female;
                return HumanGender.Unknown;
            }
        }

        public static FacebookGraph Deserialize(string json)
        {
            if (string.IsNullOrEmpty(json)) throw new ArgumentNullException("json");
            return Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(json)));
        }

        public static FacebookGraph Deserialize(Stream jsonStream)
        {
            if (jsonStream == null) throw new ArgumentNullException("jsonStream");
            return (FacebookGraph)jsonSerializer.ReadObject(jsonStream);
        }

        public static class Fields
        {
            public const string Defaults = "id,name,first_name,middle_name,last_name,gender,locale,link,username";
            public const string Birthday = "locale,birthday";
            public const string Email = "email";
            public const string Picture = "picture";
        }

        [Obsolete]
        [DataContract]
        public class FacebookPicture
        {
            [DataMember(Name = "data")]
            public FacebookPictureData Data { get; set; }
        }

        [Obsolete]
        [DataContract]
        public class FacebookPictureData
        {
            [DataMember(Name = "url")]
            public Uri Url { get; set; }
            [DataMember(Name = "is_silhouette")]
            public bool IsSilhouette { get; set; }
        }

        [DataContract]
        public class FacebookIdName
        {
            [DataMember(Name = "id")]
            public string Id { get; set; }
            [DataMember(Name = "name")]
            public string Name { get; set; }
        }
    }
}
