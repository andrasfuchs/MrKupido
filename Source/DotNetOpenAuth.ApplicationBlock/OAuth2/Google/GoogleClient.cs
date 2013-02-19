//-----------------------------------------------------------------------
// <copyright file="FacebookClient.cs" company="Outercurve Foundation">
//     Copyright (c) Outercurve Foundation. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace DotNetOpenAuth.ApplicationBlock {
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Web;
	using DotNetOpenAuth.Messaging;
	using DotNetOpenAuth.OAuth2;
    using System.Net;
    using System.IO;

    // https://accounts.google.com/o/oauth2/auth

	public class GoogleClient : WebServerClient {
		private static readonly AuthorizationServerDescription GoogleDescription = new AuthorizationServerDescription {
            TokenEndpoint = new Uri("https://accounts.google.com/o/oauth2/token"),
            AuthorizationEndpoint = new Uri("https://accounts.google.com/o/oauth2/auth"),
            //RevokeEndpoint = new Uri("https://accounts.google.com/o/oauth2/revoke")
		};

		/// <summary>
        /// Initializes a new instance of the <see cref="GoogleClient"/> class.
		/// </summary>
        public GoogleClient()
            : base(GoogleDescription)
        {
        }

        public IOAuth2Graph GetGraph(IAuthorizationState authState, string[] fields = null)
        {
            if ((authState != null) && (authState.AccessToken != null))
            {
                WebRequest request = WebRequest.Create("https://www.googleapis.com/oauth2/v1/userinfo?access_token=" + Uri.EscapeDataString(authState.AccessToken));
                WebResponse response = request.GetResponse();

                if (response != null)
                {
                    Stream responseStream = response.GetResponseStream();

                    if (responseStream != null)
                    {
                        return GoogleGraph.Deserialize(responseStream);
                    }
                }
            }

            return null;
        }        

        /// <summary>
        /// Well-known scopes defined by Google.
        /// </summary>
        /// <remarks>
        /// This sample includes just a few scopes.  For a complete list of permissions please refer to:
        /// https://developers.google.com/accounts/docs/OAuth2Login
        /// </remarks>
        public static class Scopes
        {            
            public static class UserInfo
            {
                /// <summary>
                /// Gain read-only access to basic profile information, including a user identifier, name, profile photo, profile URL, country, language, timezone, and birthdate.
                /// </summary>
                public const string Profile = "https://www.googleapis.com/auth/userinfo.profile";

                /// <summary>
                /// Gain read-only access to the user's email address.
                /// </summary>
                public const string Email = "https://www.googleapis.com/auth/userinfo.email";
            }

            public const string PlusMe = "https://www.googleapis.com/auth/plus.me";

            public static class Drive
            {
                /// <summary>
                /// Full, permissive scope to access all of a user's files. Request this scope only when it is strictly necessary.
                /// </summary>
                public const string Default = "https://www.googleapis.com/auth/drive";

                /// <summary>
                /// Per-file access to files created or opened by the app
                /// </summary>
                public const string File = "https://www.googleapis.com/auth/drive.file";
            }
        }

	}
}
