using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using MrKupido.WebCore.Authentication;

namespace MrKupido.WebCore.Authentication
{
    public class GoogleTokenResponse
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string refresh_token { get; set; }
        public string scope { get; set; }
        public string id_token { get; set; }
    }

    public class GoogleClient
    {
        private const string TokenEndpoint = "https://accounts.google.com/o/oauth2/token";
        private const string AuthorizationEndpoint = "https://accounts.google.com/o/oauth2/auth";
        private const string UserInfoEndpoint = "https://www.googleapis.com/oauth2/v1/userinfo";

        public GoogleClient() { }

        public string GetAuthorizationUrl(string clientId, string redirectUri, string scope)
        {
            return $"{AuthorizationEndpoint}?client_id={Uri.EscapeDataString(clientId)}&redirect_uri={Uri.EscapeDataString(redirectUri)}&response_type=code&scope={Uri.EscapeDataString(scope)}";
        }

        public async Task<GoogleTokenResponse> ExchangeCodeForTokenAsync(string clientId, string clientSecret, string redirectUri, string code)
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
                return JsonSerializer.Deserialize<GoogleTokenResponse>(content);
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
                return GoogleGraph.Deserialize(stream);
            }
        }

        public static class Scopes
        {
            public static class UserInfo
            {
                public const string Profile = "https://www.googleapis.com/auth/userinfo.profile";
                public const string Email = "https://www.googleapis.com/auth/userinfo.email";
            }
        }
    }
}
