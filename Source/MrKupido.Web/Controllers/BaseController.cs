using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MrKupido.Model;
using System.Web.Routing;
using System.Web.Security;
using MrKupido.Web.Models;
using System.Net;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.ComponentModel;

namespace MrKupido.Web.Controllers
{
    public class BaseController : Controller
    {
        private static MrKupido.DataAccess.MrKupidoContext context = new MrKupido.DataAccess.MrKupidoContext("Name=MrKupidoContext");
        public static Dictionary<string, UserState> CurrentSessions = new Dictionary<string, UserState>();

        private static DateTime logMustBeWrittenAt = DateTime.MinValue;
        private static int logItemsToWrite = 0;

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

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

            lock (CurrentSessions)
            {
                if (!CurrentSessions.ContainsKey(requestContext.HttpContext.Session.SessionID))
                {
                    CurrentSessions.Add(requestContext.HttpContext.Session.SessionID, new UserState());

                    lock (CurrentSessions[requestContext.HttpContext.Session.SessionID])
                    {
                        CurrentSessions[requestContext.HttpContext.Session.SessionID].Changed += new UserStateChangedEventHandler(BaseController_UserStateChanged);
                        CurrentSessions[requestContext.HttpContext.Session.SessionID].SessionID = requestContext.HttpContext.Session.SessionID;
                    }
                }
                CurrentSessions[requestContext.HttpContext.Session.SessionID].IPAddress = GetIPAddress(Request);
            }


            CurrentSessions[requestContext.HttpContext.Session.SessionID].RequestContext = requestContext;
            // Grab the user's login information from FormsAuth
            if (this.User.Identity != null && this.User.Identity is FormsIdentity)
            {
                User loggedInUser = CurrentSessions[requestContext.HttpContext.Session.SessionID].User.FromJSONString(((FormsIdentity)this.User.Identity).Ticket.UserData);

                if ((CurrentSessions[requestContext.HttpContext.Session.SessionID].User == null) || (CurrentSessions[requestContext.HttpContext.Session.SessionID].User.UserId != loggedInUser.UserId))
                {
                    CurrentSessions[requestContext.HttpContext.Session.SessionID].User = loggedInUser;
                }
            }

            if (this.Session["WebAppFileVersion"] == null)
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
                this.Session["WebAppFileVersion"] = fvi.ProductVersion;
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
                parameters += String.Format(", user-agent: '{0}'", CurrentSessions[sessionId].RequestContext.HttpContext.Request.UserAgent);
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
            Log("EXCEPTION", "User '{0}' caused an exception using the version {1}: '{2}'", Session["LastErrorMessage"] as string);
            return null;
        }

        protected void Log(string action, string formatterText, string parameters)
        {
            string username = Session.GetCurrentUser() == null ? "Anonymous" : Session.GetCurrentUser().FullName;

            Log log = new Log()
                {
                    UtcTime = DateTime.UtcNow,
                    IPAddress = CurrentSessions[HttpContext.Session.SessionID].IPAddress,
                    SessionId = HttpContext.Session.SessionID,
                    Action = action,
                    Parameters = parameters,
                    FormattedMessage = String.Format(formatterText, username, (Session["WebAppFileVersion"] == null ? "v?" : (string)Session["WebAppFileVersion"]), parameters)
                };


            LogAsync(log, true);
        }


        /// <summary>
        /// Allow external initialization of this controller by explicitly
        /// passing in a request context
        /// </summary>
        /// <param name="requestContext"></param>
        public void InitializeForced(RequestContext requestContext)
        {
            this.Initialize(requestContext);
        }


        /// <summary>
        /// Displays a self contained error page without redirecting.
        /// Depends on ErrorController.ShowError() to exist
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="redirectTo"></param>
        /// <returns></returns>
        //protected internal ActionResult DisplayErrorPage(string title, string message, string redirectTo = null)
        //{
        //ErrorController controller = new ErrorController();
        //controller.InitializeForced(this.ControllerContext.RequestContext);
        //return controller.ShowError(title, message, redirectTo);
        //}


        public static string GetIPAddress(HttpRequestBase request)
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
