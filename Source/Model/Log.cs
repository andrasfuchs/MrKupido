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
        public LogType Type { get; set; }
        [Required]
        public LogEvent Event { get; set; }

        [Required]
        public string Message { get; set; }
        [Required]
        public User User { get; set; }
    }

    public enum LogType { General, User }
    public enum LogEvent { PageLoad, ButtonClicked, DetailsOpened, DetailsClosed, LogIn, LogOut }
}
