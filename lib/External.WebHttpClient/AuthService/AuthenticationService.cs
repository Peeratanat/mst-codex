
using Common.Helper;
using Base.DTOs.Auth;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace External.WebHttpClient 
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly EnvironmentAuthenHelper _env;
        private readonly string _authority = string.Empty;
        private readonly string? _apiName;
        private readonly string? _apiSecret;
        public AuthenticationService()
        {
            _env = new EnvironmentAuthenHelper();
            _authority = _env.Authority;
            _apiName = _env.ApiName;
            _apiSecret = _env.ApiSecret;
        }

        public async Task<IntrospecDTO> GetIntrospect(string token)
        {
            try
            {
                var responseData = new IntrospecDTO();
                var client = new HttpClient();
                var baseUri = new Uri(_authority);
                client.BaseAddress = baseUri;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.ConnectionClose = true;

                //Post body content
                var values = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("token", token)
                };
                var content = new FormUrlEncodedContent(values);

                var authenticationString = $"{_apiName}:{_apiSecret}";
                var base64EncodedAuthenticationString = Convert.ToBase64String(Encoding.ASCII.GetBytes(authenticationString));

                var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/connect/introspect");
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);
                requestMessage.Content = content;

                //make the request
                var res = await client.SendAsync(requestMessage);
                res.EnsureSuccessStatusCode();
                string responseBody = res.Content.ReadAsStringAsync().Result;

                if (!string.IsNullOrWhiteSpace(responseBody))
                {
                    responseData = JsonConvert.DeserializeObject<IntrospecDTO>(responseBody);
                }
#if !DEBUG
                if (responseData?.Active == false)
                {
                    throw new UnauthorizedAccessException("Unauthorized User Info");
                }
#endif
                return responseData ?? new IntrospecDTO();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
