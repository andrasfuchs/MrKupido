namespace MrKupido.Web.Authentication
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class FacebookClient
    {
        private const string TokenEndpoint = "https://graph.facebook.com/oauth/access_token";
        private const string AuthorizationEndpoint = "https://graph.facebook.com/oauth/authorize";
        private const string GraphApiEndpoint = "https://graph.facebook.com/me";

        public FacebookClient() { }

        // Example: Get Facebook OAuth2 authorization URL
        public string GetAuthorizationUrl(string clientId, string redirectUri, string scope)
        {
            return $"{AuthorizationEndpoint}?client_id={Uri.EscapeDataString(clientId)}&redirect_uri={Uri.EscapeDataString(redirectUri)}&response_type=code&scope={Uri.EscapeDataString(scope)}";
        }

        // Example: Exchange code for access token
        public async Task<string> ExchangeCodeForTokenAsync(string clientId, string clientSecret, string redirectUri, string code)
        {
            using (var client = new HttpClient())
            {
                var requestUri = $"{TokenEndpoint}?client_id={Uri.EscapeDataString(clientId)}&redirect_uri={Uri.EscapeDataString(redirectUri)}&client_secret={Uri.EscapeDataString(clientSecret)}&code={Uri.EscapeDataString(code)}";
                var response = await client.GetAsync(requestUri);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                // Parse access_token from content (JSON or query string)
                // ...parse logic here...
                return content;
            }
        }

        // Example: Get user info from Graph API
        public async Task<IOAuth2Graph> GetGraphAsync(string accessToken, string[] fields = null)
        {
            string fieldsStr = (fields == null || fields.Length == 0) ? "id,name,email" : string.Join(",", fields);
            using (var client = new HttpClient())
            {
                var requestUri = $"{GraphApiEndpoint}?access_token={Uri.EscapeDataString(accessToken)}&fields={fieldsStr}";
                var response = await client.GetAsync(requestUri);
                response.EnsureSuccessStatusCode();
                var stream = await response.Content.ReadAsStreamAsync();
                return FacebookGraph.Deserialize(stream);
            }
        }

        /// <summary>
        /// Well-known permissions defined by Facebook.
        /// </summary>
        /// <remarks>
        /// This sample includes just a few permissions.  For a complete list of permissions please refer to:
        /// https://developers.facebook.com/docs/reference/login/
        /// </remarks>
        public static class Scopes
        {
            #region Email Permissions
            /// <summary>
            /// Provides access to the user's primary email address in the email property. Do not spam users. Your use of email must comply both with Facebook policies and with the CAN-SPAM Act.
            /// </summary>
            public const string Email = "email";
            #endregion

            #region Extended Permissions
            /// <summary>
            /// Provides access to any friend lists the user created. All user's friends are provided as part of basic data, this extended permission grants access to the lists of friends a user has created, and should only be requested if your application utilizes lists of friends.
            /// </summary>
            public const string ReadFriendlists = "read_friendlists";

            /// <summary>
            /// Provides read access to the Insights data for pages, applications, and domains the user owns.
            /// </summary>
            public const string ReadInsights = "read_insights";
            #endregion

            #region Extended Profile Properties
            /// <summary>
            /// Provides access to the "About Me" section of the profile in the about property
            /// </summary>
            public const string UserAboutMe = "user_about_me";

            /// <summary>
            /// Provides access to the user's list of activities as the activities connection
            /// </summary>
            public const string UserActivities = "user_activities";

            /// <summary>
            /// Provides access to the birthday with year as the birthday property. Note that your app may determine if a user is "old enough" to use an app by obtaining the age_range public profile property
            /// </summary>
            public const string UserBirthday = "user_birthday";
            #endregion

            #region Open Graph Permissions
            #endregion

            #region Page Permissions
            #endregion

            #region Public Profile and Friend List
            #endregion
        }
    }
}
