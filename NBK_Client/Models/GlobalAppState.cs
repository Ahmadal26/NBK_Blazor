using System.IdentityModel.Tokens.Jwt;

using System.Net.Http.Headers;
using System.Security.Claims;

namespace NBK_Client.Models
{
    public class GlobalAppState
    {
        public string Token { get; private set; }
        public string Username { get; private set; }
        public int UserId { get; private set; }
        public bool IsLoggedIn  => Token != null && Token != "";
        public bool IsAdmin { get; private set; }

        public void SaveToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);

            // Extract username and user ID from the JWT claims
            Username = jwtSecurityToken.Claims.FirstOrDefault(p => p.Type == "username")?.Value ?? "";
            UserId = int.TryParse(jwtSecurityToken.Claims.FirstOrDefault(p => p.Type == "userId")?.Value, out var id) ? id : 0;

            // Check if the user role is Admin
            IsAdmin = jwtSecurityToken.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Role)?.Value == "Admin";

            Token = token;
        }

        public void RemoveToken()
        {
            Token = null;
            Username = "";
            UserId = 0;
            IsAdmin = false;
        }

        public HttpClient CreateClient()
        {
            var client = new HttpClient();

            // Add the Authorization header with the Bearer token if the user is logged in
            if (!string.IsNullOrEmpty(Token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            }

            // Configure the HttpClient BaseAddress
            client.BaseAddress = new Uri("https://localhost:7156"); // Modify this as per your API's base URL

            return client;
        }
    }
}
