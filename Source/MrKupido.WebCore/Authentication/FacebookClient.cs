using System;
using System.Net.Http;
using System.Threading.Tasks;
using MrKupido.WebCore.Authentication;

namespace MrKupido.WebCore.Authentication
{
    public class FacebookClient
    {
        private const string TokenEndpoint = "https://graph.facebook.com/oauth/access_token";
        private const string AuthorizationEndpoint = "https://graph.facebook.com/oauth/authorize";
        private const string GraphApiEndpoint = "https://graph.facebook.com/me";

        public FacebookClient() { }

        public string GetAuthorizationUrl(string clientId, string redirectUri, string scope)
        {
            return $"{AuthorizationEndpoint}?client_id={Uri.EscapeDataString(clientId)}&redirect_uri={Uri.EscapeDataString(redirectUri)}&response_type=code&scope={Uri.EscapeDataString(scope)}";
        }

        public async Task<string> ExchangeCodeForTokenAsync(string clientId, string clientSecret, string redirectUri, string code)
        {
            using (var client = new HttpClient())
            {
                var requestUri = $"{TokenEndpoint}?client_id={Uri.EscapeDataString(clientId)}&redirect_uri={Uri.EscapeDataString(redirectUri)}&client_secret={Uri.EscapeDataString(clientSecret)}&code={Uri.EscapeDataString(code)}";
                var response = await client.GetAsync(requestUri);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return content;
            }
        }

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

        public static class Scopes
        {
            public const string Email = "email";
            public const string UserBirthday = "user_birthday";
        }
    }
}
