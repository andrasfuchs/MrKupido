using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MrKupido.Web.Models
{
    //[DataContract(Name = "")]
    //public class WikiLocationActicle
    //{
    //    public string id { get; set; }
    //    public string lat { get; set; }
    //    public string lng { get; set; }
    //    public string type { get; set; }
    //    [DataMember(Name = "title")]
    //    public string Title { get; set; }
    //    public string url { get; set; }
    //    public string mobileurl { get; set; }
    //    public string distance { get; set; }
    //}

    //public class WikiLocationRoot
    //{
    //    [DataMember(Name = "articles")]
    //    public List<WikiLocationActicle> Articles { get; set; }
    //}


    [DataContract]
    public class Article
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }
        [DataMember(Name = "lat")]
        public string Lat { get; set; }
        [DataMember(Name = "lng")]
        public string Lng { get; set; }
        [DataMember(Name = "type")]
        public string Type { get; set; }
        [DataMember(Name = "title")]
        public string Title { get; set; }
        [DataMember(Name = "url")]
        public string Url { get; set; }
        [DataMember(Name = "mobileurl")]
        public string MobileUrl { get; set; }
        [DataMember(Name = "distance")]
        public string Distance { get; set; }
    }

    public class WikiLocationRoot
    {
        public List<Article> articles { get; set; }
    }
}