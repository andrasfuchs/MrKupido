using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MrKupido.Model;
using MrKupido.Web.Core.Models;

namespace MrKupido.Web.Core.Controllers
{
    public class BaseController : Controller
    {
        private static MrKupido.DataAccess.MrKupidoContext context = new MrKupido.DataAccess.MrKupidoContext("Name=MrKupidoContext");
        public static Dictionary<string, UserState> CurrentSessions = new Dictionary<string, UserState>();

        private static DateTime logMustBeWrittenAt = DateTime.MinValue;
        private static int logItemsToWrite = 0;

        protected string rootUrl = "http://localhost:8416";

        //public User CurrentUser
        //{
        //    set
        //    {
        //        CurrentSessions[Session.SessionID].User = value;
        //        Session["CurrentUser"] = CurrentSessions[Session.SessionID].User;
        //    }

        //    get
        //    {
        //        if (CurrentSessions[Session.SessionID].User == null)
        //        {
        //            HttpCookie authCookie = HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
        //            if (authCookie != null)
        //            {
        //                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
        //                CurrentSessions[Session.SessionID].User = CurrentSessions[Session.SessionID].User.FromJSONString(ticket.UserData);
        //            }
        //            else
        //            {
        //                // redirect to login
        //                CurrentSessions[Session.SessionID].User = null;
        //            }
        //        }

        //        Session["CurrentUser"] = CurrentSessions[Session.SessionID].User;
        //        return CurrentSessions[Session.SessionID].User;
        //    }
        //}

        protected override void Initialize(HttpContext requestContext)
        {
            base.Initialize(requestContext);

            string sessionId = requestContext.Session.Id;
            lock (CurrentSessions)
            {
                if (!CurrentSessions.ContainsKey(sessionId))
                {
                    CurrentSessions.Add(sessionId, new UserState());

                    lock (CurrentSessions[sessionId])
                    {
                        CurrentSessions[sessionId].Changed += new UserStateChangedEventHandler(BaseController_UserStateChanged);
                        CurrentSessions[sessionId].SessionID = sessionId;
                    }
                }
                CurrentSessions[sessionId].IPAddress = GetIPAddress(HttpContext.Request);
            }


            CurrentSessions[sessionId].RequestContext = requestContext;
            // Grab the user's login information from FormsAuth
            if (this.User.Identity != null && this.User.Identity is FormsIdentity)
            {
                User loggedInUser = CurrentSessions[sessionId].User.FromJSONString(((FormsIdentity)this.User.Identity).Ticket.UserData);

                if ((CurrentSessions[sessionId].User == null) || (CurrentSessions[sessionId].User.UserId != loggedInUser.UserId))
                {
                    CurrentSessions[sessionId].User = loggedInUser;
                }
            }

            if (requestContext.Session.GetString("WebAppFileVersion") == null)
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
                requestContext.Session.SetString("WebAppFileVersion", fvi.ProductVersion);
            }
        }

        void BaseController_UserStateChanged(object sender, DateTime utc, string ip, string sessionId, string action, string parameters)
        {
            string fm = "";
            bool forceDBWrite = true;
            string username = CurrentSessions[sessionId].User == null ? "Anonymous" : CurrentSessions[sessionId].User.FullName;

            if (action == "LOGIN")
            {
                fm = String.Format("User '{0}' logged in.", username);
                parameters += String.Format(", user-agent: '{0}'", CurrentSessions[sessionId].RequestContext.Request.UserAgent);
            }

            if (action == "LOGOUT")
            {
                fm = String.Format("User '{0}' logged out.", username);
            }

            if (action.StartsWith("URL:"))
            {
                fm = String.Format("The {0} was requested by '{1}'.", action, username);
                forceDBWrite = false;
            }

            Log log = new Log() { UtcTime = utc, IPAddress = ip, SessionId = sessionId, Action = action, Parameters = parameters, FormattedMessage = fm };
            LogAsync(log, forceDBWrite);
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
        public ActionResult ReportBug(string text)
        {
            Log("BUGREPORT", "User '{0}' reported the following bug using the version {1}: '{2}'", text);
            return null;
        }

        [HttpPost]
        public ActionResult LogException()
        {
            Log("EXCEPTION", "User '{0}' caused an exception using the version {1}: '{2}'", HttpContext.Session.GetString("LastErrorMessage"));
            return null;
        }

        protected void Log(string action, string formatterText, string parameters)
        {
            string username = HttpContext.Session.GetCurrentUser() == null ? "Anonymous" : HttpContext.Session.GetCurrentUser().FullName;

            string webAppVersion = HttpContext.Session.GetString("WebAppFileVersion");

            Log log = new Log()
            {
                UtcTime = DateTime.UtcNow,
                IPAddress = CurrentSessions[HttpContext.Session.Id].IPAddress,
                SessionId = HttpContext.Session.Id,
                Action = action,
                Parameters = parameters,
                FormattedMessage = String.Format(formatterText, username, (webAppVersion == null ? "v?" : webAppVersion), parameters)
            };


            LogAsync(log, true);
        }

        public static string GetIPAddress(HttpRequest request)
        {
            string szRemoteAddr = request.UserHostAddress;
            string szXForwardedFor = request.Headers["Via"] ?? request.ServerVariables["X_FORWARDED_FOR"];
            string szIP = "";

            if (szXForwardedFor == null)
            {
                szIP = szRemoteAddr;
            }
            else
            {
                szIP = szXForwardedFor;
            }
            return szIP;
        }
    }
}
