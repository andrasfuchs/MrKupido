using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.RelyingParty;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using System.Web.Security;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
using MrKupido.Model;

namespace Web.Controllers
{
    public class AccountController : Controller
    {
        MrKupido.DataAccess.MrKupidoContext context = new MrKupido.DataAccess.MrKupidoContext();

        [HttpGet]
        public ActionResult LogIn()
        {
            var openid = new OpenIdRelyingParty();
            IAuthenticationResponse response = openid.GetResponse();

            if (response != null)
            {
                switch (response.Status)
                {
                    case AuthenticationStatus.Authenticated:

                        FetchResponse fetch = response.GetExtension<FetchResponse>();
                        string email = fetch == null ? null : fetch.GetAttributeValue(WellKnownAttributes.Contact.Email);


                        // check if the user already exists
                        User user = context.Users.FirstOrDefault(u => u.Email == email);

                        if (user == null)
                        {
                            // if it does, create it
                            user = new User();
                            user.Email = email;
                        }

                        if (user.FirstName == null)
                        {
                            user.FirstName = fetch == null ? null : fetch.GetAttributeValue(WellKnownAttributes.Name.First);
                        }

                        if (user.LastName == null)
                        {
                            user.LastName = fetch == null ? null : fetch.GetAttributeValue(WellKnownAttributes.Name.Last);
                        }

                        if (user.FullName == null)
                        {
                            user.FullName = fetch == null ? null : fetch.GetAttributeValue(WellKnownAttributes.Name.FullName);
                        }

                        FormsAuthentication.RedirectFromLoginPage(
                            response.ClaimedIdentifier, false);
                        break;
                    case AuthenticationStatus.Canceled:
                        ModelState.AddModelError("loginIdentifier",
                            "Login was cancelled at the provider");
                        break;
                    case AuthenticationStatus.Failed:
                        ModelState.AddModelError("loginIdentifier",
                            "Login failed using the provided OpenID identifier");
                        break;
                }
            }

            return View();
        }

        [HttpPost]
        public ActionResult LogIn(string loginIdentifier)
        {
            if (!Identifier.IsValid(loginIdentifier))
            {
                ModelState.AddModelError("loginIdentifier",
                            "The specified login identifier is invalid");
                return View();
            }
            else
            {
                var openid = new OpenIdRelyingParty();
                IAuthenticationRequest request = openid.CreateRequest(Identifier.Parse(loginIdentifier));

                FetchRequest fetchRequest = new FetchRequest();
                // Google is OK with these
                fetchRequest.Attributes.Add(new AttributeRequest(WellKnownAttributes.Contact.Email,true));
                fetchRequest.Attributes.Add(new AttributeRequest(WellKnownAttributes.Name.First, true));
                fetchRequest.Attributes.Add(new AttributeRequest(WellKnownAttributes.Name.Last, true));
                fetchRequest.Attributes.Add(new AttributeRequest(WellKnownAttributes.Preferences.Language, true));
                // but not with these
                fetchRequest.Attributes.Add(new AttributeRequest(WellKnownAttributes.Name.FullName, true));
                fetchRequest.Attributes.Add(new AttributeRequest(WellKnownAttributes.Contact.HomeAddress.Country, true));
                fetchRequest.Attributes.Add(new AttributeRequest(WellKnownAttributes.Person.Gender, true));
                fetchRequest.Attributes.Add(new AttributeRequest(WellKnownAttributes.BirthDate.Year, true));

                request.AddExtension(fetchRequest);

                return request.RedirectingResponse.AsActionResult();
            }
        }

        public ActionResult LogInWithFacebook(string loginIdentifier)
        {
            return View();
        }

        public ActionResult Profile()
        {
            return View();
        }
    }
}
