using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using MrKupido.WebCore.Authentication;

namespace MrKupido.WebCore.Authentication
{
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

        public static class Scopes
        {
            public const string SignIn = "wl.signin";
            public const string Emails = "wl.emails";
            public const string Birthday = "wl.birthday";
        }
    }
}
