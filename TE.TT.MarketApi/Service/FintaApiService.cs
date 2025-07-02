using System.Text.Json.Serialization;
using TE.TT.MarketApi.Abstarct;
using TE.TT.MarketApi.Model;

namespace TE.TT.MarketApi.Service
{
    public record TestData
    {
        [JsonPropertyName("size")]
        public int Size { get; set; }
    }
    public class FintaApiService : IFintaApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IControlTokenService _tokenService;
        public FintaApiService(HttpClient httpClient, IControlTokenService tokenService)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://platform.fintacharts.com/api/instruments/v1/");
            _tokenService = tokenService;
        }

        public async Task<AssetsDto> FetchAllData()
        {
            var responseSize = await _httpClient.GetStringAsync($"instruments?size=1");
            if (string.IsNullOrWhiteSpace(responseSize))
            {
                return new AssetsDto();
            }
            //получить size после взять все 
        }
    }
}
