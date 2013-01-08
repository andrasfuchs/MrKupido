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

        /// <summary>
        /// Contains User state information retrieved from the authentication system
        /// </summary>
        //protected User UserState = new User();

        /// <summary>
        /// ErrorDisplay control that holds page level error information
        /// </summary>
        //        protected ErrorDisplay ErrorDisplay = new ErrorDisplay();



        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

            lock (CurrentSessions)
            {
                if (!CurrentSessions.ContainsKey(requestContext.HttpContext.Session.SessionID))
                {
                    CurrentSessions.Add(requestContext.HttpContext.Session.SessionID, new UserState());
                    CurrentSessions[requestContext.HttpContext.Session.SessionID].Changed += new UserStateChangedEventHandler(BaseController_UserStateChanged);
                    CurrentSessions[requestContext.HttpContext.Session.SessionID].SessionID = requestContext.HttpContext.Session.SessionID;
                    CurrentSessions[requestContext.HttpContext.Session.SessionID].IPAddress = GetPublicIP("http://repeater.smartftp.com");
                }
            }


            // Grab the user's login information from FormsAuth
            //User userState = new User();
            if (this.User.Identity != null && this.User.Identity is FormsIdentity)
            {
                User loggedInUser = CurrentSessions[requestContext.HttpContext.Session.SessionID].User.FromJSONString(((FormsIdentity)this.User.Identity).Ticket.UserData);

                if ((CurrentSessions[requestContext.HttpContext.Session.SessionID].User == null) || (CurrentSessions[requestContext.HttpContext.Session.SessionID].User.UserId != loggedInUser.UserId))
                {
                    CurrentSessions[requestContext.HttpContext.Session.SessionID].User = loggedInUser;
                }
            }

            CurrentSessions[requestContext.HttpContext.Session.SessionID].RequestContext = requestContext;

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
            //string username = ViewData["UserState"] == null ? "Anonymous" : ((User)ViewData["UserState"]).FullName;
            string username = CurrentSessions[sessionId].User == null ? "Anonymous" : CurrentSessions[sessionId].User.FullName;

            if (action == "LOGIN")
            {
                fm = String.Format("User '{0}' logged in.", username);
            }

            if (action == "LOGOUT")
            {
                fm = String.Format("User '{0}' logged out.", username);
            }

            if (action.StartsWith("URL:"))
            {
                fm = String.Format("The {0} was requested by '{1}'.", action, username);
            }

            Log log = new Log() { UtcTime = utc, IPAddress = ip, SessionId = sessionId, Action = action, Parameters = parameters, FormattedMessage = fm };
            LogAsync(log);            
        }

        private static void LogAsync(Log log)
        {
            BackgroundWorker bgWorker = new BackgroundWorker();
            bgWorker.DoWork += (sender, e) =>
            {
                lock (context)
                {
                    context.Logs.Add((Log)e.Argument);
                    context.SaveChanges();
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

        private void Log(string action, string formatterText, string parameters)
        {
            string username = Session["CurrentUser"] == null ? "Anonymous" : ((User)Session["CurrentUser"]).FullName;

            Log log = new Log()
                {
                    UtcTime = DateTime.UtcNow,
                    IPAddress = CurrentSessions[HttpContext.Session.SessionID].IPAddress,
                    SessionId = HttpContext.Session.SessionID,
                    Action = action,
                    Parameters = parameters,
                    FormattedMessage = String.Format(formatterText, username, (Session["WebAppFileVersion"] == null ? "v?" : (string)Session["WebAppFileVersion"]), parameters)
                };


            LogAsync(log);
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


        private string GetPublicIP(string publicIpUrl)
        {
            string result = "";

            HttpWebRequest req = null;
            HttpWebResponse resp = null;

            try
            {
                //string ipCountryISO = "XX";

                // get our public IP
                req = (HttpWebRequest)HttpWebRequest.Create(publicIpUrl);
                resp = (HttpWebResponse)req.GetResponse();
                StreamReader strmReader = new StreamReader(resp.GetResponseStream());
                result = strmReader.ReadToEnd().Trim();
                resp.Close();

                //string requestUrl = "http://api.hostip.info/country.php?ip=" + publicIp;
                //// get the IP's country
                //req = (HttpWebRequest)HttpWebRequest.Create(requestUrl);
                //resp = (HttpWebResponse)req.GetResponse();
                //strmReader = new StreamReader(resp.GetResponseStream());
                //ipCountryISO = strmReader.ReadToEnd().Trim();
            }
            catch { }
            finally
            {
                if (req != null)
                {
                    req = null;
                }

                if (resp != null)
                {
                    resp.Close();
                    resp = null;
                }
            }

            return result;
        }
    }
}
