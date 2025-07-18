using MrKupido.Model;
using MrKupido.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace MrKupido.Web.Controllers
{
    public class BaseController : Controller
    {
        private static MrKupido.DataAccess.MrKupidoContext context = new MrKupido.DataAccess.MrKupidoContext("Name=MrKupidoContext");
        public static Dictionary<string, UserState> CurrentSessions = new Dictionary<string, UserState>();

        private static DateTime logMustBeWrittenAt = DateTime.MinValue;
        private static int logItemsToWrite = 0;

        protected string rootUrl => ""; // Set from configuration if needed

        protected void Log(string action, string formatterText, string parameters)
        {
            string username = "Anonymous";
            Log log = new Log()
            {
                UtcTime = DateTime.UtcNow,
                IPAddress = "", // Set from request if needed
                SessionId = "", // Set from session if needed
                Action = action,
                Parameters = parameters,
                FormattedMessage = String.Format(formatterText, username, "v?", parameters)
            };
            LogAsync(log, true);
        }

        private static void LogAsync(Log log, bool forceDBWrite)
        {
            BackgroundWorker bgWorker = new BackgroundWorker();
            bgWorker.DoWork += (sender, e) =>
            {
                lock (context)
                {
                    context.Configuration.AutoDetectChangesEnabled = false;
                    context.Logs.Add((Log)e.Argument);
                    context.Configuration.AutoDetectChangesEnabled = true;

                    if (logItemsToWrite == 0)
                    {
                        logMustBeWrittenAt = DateTime.UtcNow.AddMinutes(5);
                    }
                    logItemsToWrite++;

                    if (forceDBWrite || (logItemsToWrite > 20) || (logMustBeWrittenAt < DateTime.UtcNow))
                    {
                        context.SaveChanges();
                        logItemsToWrite = 0;
                    }
                }
            };
            bgWorker.RunWorkerAsync(log);
        }

        [HttpPost]
        public IActionResult ReportBug(string text)
        {
            Log("BUGREPORT", "User '{0}' reported the following bug using the version {1}: '{2}'", text);
            return Ok();
        }

        [HttpPost]
        public IActionResult LogException()
        {
            Log("EXCEPTION", "User '{0}' caused an exception using the version {1}: '{2}'", "");
            return Ok();
        }
    }
}
