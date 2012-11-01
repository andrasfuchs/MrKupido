using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MrKupido.Model
{
    public class Log
    {
        public int LogId { get; set; }

        [Required]
        public DateTime UtcTime { get; set; }

        [Required]
        public string IPAddress { get; set; }
        [Required]
        public string SessionId { get; set; }

        [Required]
        public string Action { get; set; }
        
        public string Parameters { get; set; }
        
        public string FormattedMessage { get; set; }
    }
}
