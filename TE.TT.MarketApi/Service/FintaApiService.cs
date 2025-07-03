using System.Net.Http.Headers;
using System.Text.Json;
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
        public FintaApiService(IHttpClientFactory httpClientFactory, IControlTokenService tokenService)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
            _tokenService = tokenService;
        }
        public async Task<AssetsDto> FetchAllData()
        {
            try
            {
                string token = await _tokenService.GetValidToken();
                if (string.IsNullOrWhiteSpace(token)) return new AssetsDto();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var responseSize = await _httpClient.GetStringAsync($"instruments?size=1");
                if (string.IsNullOrWhiteSpace(responseSize))
                {
                    return new AssetsDto();
                }
                var result = JsonSerializer.Deserialize<pagingDto>(responseSize);
                int dataCount = result.Items;
                token = await _tokenService.GetValidToken();
                if (string.IsNullOrWhiteSpace(token)) return new AssetsDto();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var responseData = await _httpClient.GetStringAsync($"instruments?size={dataCount}");
                if (string.IsNullOrWhiteSpace(responseData))
                {
                    return new AssetsDto();
                }

                var resultData = JsonSerializer.Deserialize<AssetsDto>(responseData);
                if (resultData == null)
                {
                    return new AssetsDto();
                }

                return resultData;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка в FetchAllData:" + ex.Message);
                return new AssetsDto();
            }
        }
    }
}
