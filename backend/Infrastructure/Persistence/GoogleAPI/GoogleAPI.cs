// using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace Carsharing.Persistence.GoogleAPI
{
    public static class GoogleApi
    {
        public static async Task<GetTokenResult?> GetTokenAsync(string code, string appId, string appSecret, string redirectUri)
        {
            using (HttpClient client = new HttpClient())
            {
#pragma warning disable S1075 // URIs should not be hardcoded    
                const string url = "https://accounts.google.com/o/oauth2/token";
#pragma warning restore S1075 // URIs should not be hardcoded

                object contentObject = new
                {
                    code,
                    client_id = appId,
                    client_secret = appSecret,
                    redirect_uri = redirectUri,
                    grant_type = "authorization_code"
                };

                JsonContent content = JsonContent.Create(contentObject);
                using var request = new HttpRequestMessage(HttpMethod.Post, url);
                request.Content = content;
                using var response = await client.SendAsync(request);

                response.EnsureSuccessStatusCode(); // Вроде бы необязательно

                string responseText = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<GetTokenResult>(responseText); // JsonConvert.DeserializeObject 
            }
        }

        public static async Task<GetUserResult?> GetUserAsync(string accessToken, string tokenId)
        {
            using (HttpClient client = new HttpClient())
            {
#pragma warning disable S1075 // URIs should not be hardcoded    
                var url = "https://www.googleapis.com/oauth2/v1/userinfo";
#pragma warning restore S1075 // URIs should not be hardcoded
                url += "?alt=json";
                url += $"&access_token={accessToken}";

                using var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokenId);
                using var response = await client.SendAsync(request);

                response.EnsureSuccessStatusCode(); // Вроде бы необязательно

                var responseText = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<GetUserResult>(responseText);
            }
        }
    }

    public record GetTokenResult
    {
        public string access_token { get; set; } = String.Empty;
        public int expires_in { get; set; }
        public string scope { get; set; } = String.Empty;
        public string token_type { get; set; } = String.Empty;
        public string id_token { get; set; } = String.Empty;
    }

    public record GetUserResult
    {
        public string id { get; set; } = String.Empty;
        public string email { get; set; } = String.Empty;
        public bool verified_email { get; set; }
        public string name { get; set; } = String.Empty;
        public DateTime birthday { get; set; } = DateTime.UtcNow;
        public string given_name { get; set; } = String.Empty;
        public string family_name { get; set; } = String.Empty;
        public string picture { get; set; } = String.Empty;
        public string locale { get; set; } = String.Empty;
    }
}
