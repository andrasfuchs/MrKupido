﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Configuration;
using System.IO;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using DotNetOpenAuth.ApplicationBlock;
using DotNetOpenAuth.ApplicationBlock.Facebook;
using DotNetOpenAuth.OAuth2;
using DotNetOpenAuth.OpenId.RelyingParty;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
using DotNetOpenAuth.OpenId;
using MrKupido.Model;
using MrKupido.Web.Models;

namespace MrKupido.Web.Controllers
{
    public class AccountController : BaseController
    {
        private static MrKupido.DataAccess.MrKupidoContext context = new MrKupido.DataAccess.MrKupidoContext("Name=MrKupidoContext");

        private static readonly FacebookClient client = new FacebookClient
        {
            ClientIdentifier = ConfigurationManager.AppSettings["FacebookAppID"],
            ClientCredentialApplicator = ClientCredentialApplicator.PostParameter(ConfigurationManager.AppSettings["FacebookAppSecret"])
        };

        [HttpGet]
        public ActionResult LogIn()
        {
            string email = "";
            string id = "";
            FetchResponse openIdFetch = null;
            Identifier openIdIdentifier = null;
            FacebookGraph facebookGraph = null;

            if ((string)Session["LoginType"] == "OpenId")
            {
                var openid = new OpenIdRelyingParty();
                IAuthenticationResponse response = openid.GetResponse();

                if (response != null)
                {
                    switch (response.Status)
                    {
                        case AuthenticationStatus.Authenticated:
                            openIdIdentifier = response.ClaimedIdentifier;
                            id = openIdIdentifier.ToString();
                            openIdFetch = response.GetExtension<FetchResponse>();
                            email = openIdFetch == null ? null : openIdFetch.GetAttributeValue(WellKnownAttributes.Contact.Email);
                            break;
                        case AuthenticationStatus.Canceled:
                            ModelState.AddModelError("loginIdentifier", "Login was cancelled at the provider");
                            break;
                        case AuthenticationStatus.Failed:
                            ModelState.AddModelError("loginIdentifier", "Login failed using the provided OpenID identifier");
                            break;
                    }
                }
            }

            if ((string)Session["LoginType"] == "Facebook")
            {
                IAuthorizationState authorization = client.ProcessUserAuthorization();

                if (authorization != null)
                {
                    WebRequest request = WebRequest.Create("https://graph.Facebook.com/me?fields=first_name,last_name,name,picture,email,birthday,gender,locale&access_token=" + Uri.EscapeDataString(authorization.AccessToken));
                    WebResponse response = request.GetResponse();
                    
                    if (response != null)
                    {
                        Stream responseStream = response.GetResponseStream();

                        if (responseStream != null)
                        {
                            //// convert stream to string
                            //StreamReader reader = new StreamReader(responseStream);
                            //string responseText = reader.ReadToEnd();

                            facebookGraph = FacebookGraph.Deserialize(responseStream);

                            email = facebookGraph == null ? null : facebookGraph.Email;
                            id = facebookGraph == null ? null : facebookGraph.Id.ToString();
                        }
                    }
                }
            }

            // we have a valid login
            if (email != "")
            {
                // check if the user already exists
                User user = context.Users.FirstOrDefault(u => u.Email == email);

                if (user == null)
                {
                    // if it does, create it
                    user = new User();
                    user.Email = email;
                }

                if (((string)Session["LoginType"] == "OpenId") && (openIdFetch != null))
                {

                    if (String.IsNullOrEmpty(user.CultureName))
                    {
                        user.CultureName = openIdFetch.GetAttributeValue(WellKnownAttributes.Preferences.Language);
                    }

                    if (String.IsNullOrEmpty(user.FirstName))
                    {
                        user.FirstName = openIdFetch.GetAttributeValue(WellKnownAttributes.Name.First);
                    }

                    if (String.IsNullOrEmpty(user.LastName))
                    {
                        user.LastName = openIdFetch.GetAttributeValue(WellKnownAttributes.Name.Last);
                    }

                    if (String.IsNullOrEmpty(user.FullName))
                    {
                        user.FullName = openIdFetch.GetAttributeValue(WellKnownAttributes.Name.FullName);
                    }

                    if (user.Gender == (int)MrKupido.Model.Gender.Unknown)
                    {
                        string gender = openIdFetch.GetAttributeValue(WellKnownAttributes.Person.Gender);

                        if (gender == "M") user.Gender = (int)MrKupido.Model.Gender.Male;
                        if (gender == "F") user.Gender = (int)MrKupido.Model.Gender.Female;
                    }

                    if (user.DateOfBirth == null)
                    {
                        string birthdate = openIdFetch.GetAttributeValue(WellKnownAttributes.BirthDate.WholeBirthDate);
                    }
                }

                if (((string)Session["LoginType"] == "Facebook") && (facebookGraph != null))
                {
                    if (String.IsNullOrEmpty(user.CultureName))
                    {
                        user.CultureName = facebookGraph.Locale.Replace('_', '-');
                    }

                    if (String.IsNullOrEmpty(user.FirstName))
                    {
                        user.FirstName = HttpUtility.HtmlDecode(facebookGraph.FirstName);
                    }

                    if (String.IsNullOrEmpty(user.LastName))
                    {
                        user.LastName = HttpUtility.HtmlDecode(facebookGraph.LastName);
                    }

                    if (String.IsNullOrEmpty(user.FullName))
                    {
                        user.FullName = HttpUtility.HtmlDecode(facebookGraph.Name);
                    }

                    if (user.Gender == (int)MrKupido.Model.Gender.Unknown)
                    {
                        string gender = facebookGraph.Gender;

                        if (gender == "male") user.Gender = (int)MrKupido.Model.Gender.Male;
                        if (gender == "female") user.Gender = (int)MrKupido.Model.Gender.Female;
                    }

                    if (user.DateOfBirth == null)
                    {
                        user.DateOfBirth = DateTime.ParseExact(facebookGraph.Birthday, "MM/dd/yyyy", null);
                    }
                }

                if (String.IsNullOrEmpty(user.FullName))
                {
                    if (user.CultureName == "hu")
                    {
                        user.FullName = user.LastName + " " + user.FirstName;
                    }
                    else
                    {
                        user.FullName = user.FirstName + " " + user.LastName;
                    }
                }

                if (user.UserId == 0)
                {
                    context.Users.Add(user);
                }

                context.SaveChanges();

                this.IssueAuthTicket(user.UserId.ToString(), user, true);

                Session["CurrentUser"] = user;
                Session["CurrentUser.DisplayName"] = !String.IsNullOrEmpty(user.NickName) ? user.NickName : user.FullName;

                string returnUrl = (string)Session["ReturnUrl"];
                if (String.IsNullOrEmpty(returnUrl))
                {
                    //return RedirectToRoute("Default");
                    return RedirectToRoute("Default", new { language = "hun", controller = "Home", action = "Index" });
                }
                else
                {
                    Session["ReturnUrl"] = null;
                    return new RedirectResult(returnUrl);
                }
            }

            Session["ReturnUrl"] = HttpContext.Request.QueryString["ReturnUrl"];

            return View();
        }

        /// <summary>
        /// Issues an authentication issue from a userState instance
        /// </summary>
        /// <param name="userState"></param>
        /// <param name="rememberMe"></param>
        private void IssueAuthTicket(string id, User userState, bool rememberMe)
        {
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, id,
                                                                 DateTime.Now, DateTime.Now.AddDays(10),
                                                                 rememberMe, userState.ToJSONString());

            string ticketString = FormsAuthentication.Encrypt(ticket);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, ticketString);
            if (rememberMe) cookie.Expires = DateTime.Now.AddDays(10);

            HttpContext.Response.Cookies.Add(cookie);
        }

        [HttpPost]
        public ActionResult LogIn(string loginIdentifier)
        {
            if (loginIdentifier == "Facebook")
            {
                return LogInWithFacebook();
            }
            else
            {
                // OpenId
                if (!Identifier.IsValid(loginIdentifier))
                {
                    ModelState.AddModelError("loginIdentifier", "The specified login identifier is invalid");
                    return View();
                }
                else
                {
                    Session["LoginType"] = "OpenId";

                    var openid = new OpenIdRelyingParty();
                    IAuthenticationRequest request = openid.CreateRequest(Identifier.Parse(loginIdentifier));

                    FetchRequest fetchRequest = new FetchRequest();
                    // Google is OK with these
                    fetchRequest.Attributes.Add(new AttributeRequest(WellKnownAttributes.Contact.Email, true));
                    fetchRequest.Attributes.Add(new AttributeRequest(WellKnownAttributes.Name.First, true));
                    fetchRequest.Attributes.Add(new AttributeRequest(WellKnownAttributes.Name.Last, true));
                    fetchRequest.Attributes.Add(new AttributeRequest(WellKnownAttributes.Preferences.Language, true));
                    // but not with these
                    fetchRequest.Attributes.Add(new AttributeRequest(WellKnownAttributes.Name.FullName, true));
                    fetchRequest.Attributes.Add(new AttributeRequest(WellKnownAttributes.Person.Gender, true));
                    fetchRequest.Attributes.Add(new AttributeRequest(WellKnownAttributes.BirthDate.WholeBirthDate, true));

                    request.AddExtension(fetchRequest);

                    return request.RedirectingResponse.AsActionResult();
                }
            }
        }

        public ActionResult LogInWithFacebook()
        {
            Session["LoginType"] = "Facebook";

            IAuthorizationState authorization = client.ProcessUserAuthorization();
            if (authorization == null)
            {
                // Kick off authorization request
                client.RequestUserAuthorization(new string[] { "email", "user_birthday" });
            }
            return View();
        }

        [HttpGet]
        public new ActionResult Profile()
        {
            if (Session["CurrentUser"] == null)
            {
                return RedirectToAction("LogIn");
            }

            return View();
        }

        [HttpPost]
        public new ActionResult Profile(string userId)
        {
            User user = (User)Session["CurrentUser"];

            user.LastName = Request.Form["lastname"];
            user.FirstName = Request.Form["firstname"];
            user.NickName = Request.Form["nickname"];

            context.SaveChanges();

            Session["CurrentUser.DisplayName"] = !String.IsNullOrEmpty(user.NickName) ? user.NickName : user.FullName;

            return View();
        }
    }
}
