using DataLayer.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DayColor
{
    public class DayColorService
    {
        public const string ClientName = "tempo";
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IAuthService _authService;

        public DayColorService(IHttpClientFactory httpClientFactory, IAuthService authService)
        {
            _httpClientFactory = httpClientFactory;
            _authService = authService;
        }

        public async Task<TempoLikeResponse> GetDayColorAsync()
        {
            var token = await _authService.AuthenticateAsync();
            var client = _httpClientFactory.CreateClient(ClientName);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(token.token_type, token.access_token);
            var response = await client.GetAsync("/open_api/tempo_like_supply_contract/v1/tempo_like_calendars");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<TempoLikeResponse>();
                if (result != null)
                {
                    return result;
                }
            }
            throw new InvalidOperationException($"Impossible to get day data : {response.ReasonPhrase}");
        }
    }
}
