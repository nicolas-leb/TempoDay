using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Auth
{
    public class AuthService : IAuthService
    {
        public const string ClientName = "auth";
        private readonly IHttpClientFactory _httpClientFactory;
        private Token? _token;
        private DateTime? _tokenExpiration;

        public AuthService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Token> AuthenticateAsync()
        {
            if (_token == null || DateTime.Now >= _tokenExpiration)
            {
                var client = _httpClientFactory.CreateClient(ClientName);

                var response = await client.PostAsync("/token/oauth/", null);

                if (response.IsSuccessStatusCode)
                {
                    var token = await response.Content.ReadFromJsonAsync<Token>();
                    if (token is not null)
                    {
                        _token = token;
                        _tokenExpiration = DateTime.Now.AddSeconds(_token.expires_in);
                        return _token;
                    }
                }
                throw new InvalidOperationException($"Impossible to authenticate : {response.ReasonPhrase}");
            }
            return _token;
        }
    }
}
