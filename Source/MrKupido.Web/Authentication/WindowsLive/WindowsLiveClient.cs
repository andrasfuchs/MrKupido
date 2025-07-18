namespace MrKupido.Web.Authentication
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class WindowsLiveClient
    {
        private const string TokenEndpoint = "https://oauth.live.com/token";
        private const string AuthorizationEndpoint = "https://oauth.live.com/authorize";
        private const string UserInfoEndpoint = "https://apis.live.net/v5.0/me";

        public WindowsLiveClient() { }

        public string GetAuthorizationUrl(string clientId, string redirectUri, string scope)
        {
            return $"{AuthorizationEndpoint}?client_id={Uri.EscapeDataString(clientId)}&redirect_uri={Uri.EscapeDataString(redirectUri)}&response_type=code&scope={Uri.EscapeDataString(scope)}";
        }

        public async Task<string> ExchangeCodeForTokenAsync(string clientId, string clientSecret, string redirectUri, string code)
        {
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Post, TokenEndpoint)
                {
                    Content = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("code", code),
                        new KeyValuePair<string, string>("client_id", clientId),
                        new KeyValuePair<string, string>("client_secret", clientSecret),
                        new KeyValuePair<string, string>("redirect_uri", redirectUri),
                        new KeyValuePair<string, string>("grant_type", "authorization_code")
                    })
                };
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                // Parse access_token from content (JSON)
                // ...parse logic here...
                return content;
            }
        }

        public async Task<IOAuth2Graph> GetGraphAsync(string accessToken)
        {
            using (var client = new HttpClient())
            {
                var requestUri = $"{UserInfoEndpoint}?access_token={Uri.EscapeDataString(accessToken)}";
                var response = await client.GetAsync(requestUri);
                response.EnsureSuccessStatusCode();
                var stream = await response.Content.ReadAsStreamAsync();
                var windowsLiveGraph = WindowsLiveGraph.Deserialize(stream);
                windowsLiveGraph.AvatarUrl = new Uri($"https://apis.live.net/v5.0/me/picture?access_token={Uri.EscapeDataString(accessToken)}");
                return windowsLiveGraph;
            }
        }

        /// <summary>
        /// Well-known scopes defined by the Windows Live service.
        /// </summary>
        /// <remarks>
        /// This sample includes just a few scopes.  For a complete list of scopes please refer to:
        /// http://msdn.microsoft.com/en-us/library/hh243646.aspx
        /// </remarks>
        public static class Scopes
        {
            #region Core Scopes

            /// <summary>
			/// The ability of an app to read and update a user's info at any time. Without this scope, an app can access the user's info only while the user is signed in to Live Connect and is using your app.
			/// </summary>
			public const string OfflineAccess = "wl.offline_access";

            /// <summary>
            /// Single sign-in behavior. With single sign-in, users who are already signed in to Live Connect are also signed in to your website.
            /// </summary>
            public const string SignIn = "wl.signin";

            /// <summary>
            /// Read access to a user's basic profile info. Also enables read access to a user's list of contacts.
            /// </summary>
            public const string Basic = "wl.basic";

            #endregion

            #region Extended Scopes

            /// <summary>
            /// Read access to a user's birthday info including birth day, month, and year.
            /// </summary>
            public const string Birthday = "wl.birthday";

            /// <summary>
            /// Read access to a user's calendars and events.
            /// </summary>
            public const string Calendars = "wl.calendars";

            /// <summary>
            /// Read and write access to a user's calendars and events.
            /// </summary>
            public const string CalendarsUpdate = "wl.calendars_update";

            /// <summary>
            /// Read access to the birth day and birth month of a user's contacts. Note that this also gives read access to the user's birth day, birth month, and birth year.
            /// </summary>
            public const string ContactsBirthday = "wl.contacts_birthday";

            /// <summary>
            /// Creation of new contacts in the user's address book.
            /// </summary>
            public const string ContactsCreate = "wl.contacts_create";

            /// <summary>
            /// Read access to a user's calendars and events. Also enables read access to any calendars and events that other users have shared with the user.
            /// </summary>
            public const string ContactsCalendars = "wl.contacts_calendars";

            /// <summary>
            /// Read access to a user's albums, photos, videos, and audio, and their associated comments and tags. Also enables read access to any albums, photos, videos, and audio that other users have shared with the user.
            /// </summary>
            public const string ContactsPhotos = "wl.contacts_photos";

            /// <summary>
            /// Read access to Microsoft SkyDrive files that other users have shared with the user. Note that this also gives read access to the user's files stored in SkyDrive.
            /// </summary>
            public const string ContactsSkydrive = "wl.contacts_skydrive";

            /// <summary>
            /// Read access to a user's personal, preferred, and business email addresses.
            /// </summary>
            public const string Emails = "wl.emails";


            /// <summary>
            /// Creation of events on the user's default calendar.
            /// </summary>
            public const string EventsCreate = "wl.events_create";

            /// <summary>
            /// Enables signing in to the Windows Live Messenger Extensible Messaging and Presence Protocol (XMPP) service.
            /// </summary>
            public const string Messenger = "wl.messenger";

            /// <summary>
            /// Read access to a user's personal, business, and mobile phone numbers.
            /// </summary>
            public const string PhoneNumbers = "wl.phone_numbers";

            /// <summary>
            /// Read access to a user's photos, videos, audio, and albums.
            /// </summary>
            public const string Photos = "wl.photos";

            /// <summary>
            /// Read access to a user's postal addresses.
            /// </summary>
            public const string PostalAddresses = "wl.postal_addresses";

            /// <summary>
            /// Enables updating a user's status message.
            /// </summary>
            public const string Share = "wl.share";

            /// <summary>
            /// Read access to a user's files stored in SkyDrive.
            /// </summary>
            public const string Skydrive = "wl.skydrive";

            /// <summary>
            /// Read and write access to a user's files stored in SkyDrive.
            /// </summary>
            public const string SkydriveUpdate = "wl.skydrive_update";

            /// <summary>
            /// Read access to a user's employer and work position information.
            /// </summary>
            public const string WorkProfile = "wl.work_profile";

            #endregion
        }
    }
}
