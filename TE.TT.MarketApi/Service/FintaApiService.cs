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
                var result = JsonSerializer.Deserialize<TakePagingDto>(responseSize);
                int dataCount = result.Paging.Items;
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
                Console.WriteLine("Error in  FetchAllData:" + ex.Message);
                return new AssetsDto();
            }
        }

        public async Task<ProvidersDto> FetchDataProviders()
        {
            try
            {
                string token = await _tokenService.GetValidToken();
                if (string.IsNullOrWhiteSpace(token))
                {
                    Console.WriteLine("Error provide token null");
                    return new ProvidersDto();
                }
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetStringAsync("providers");
                if (string.IsNullOrWhiteSpace(response))
                {
                    return new ProvidersDto();
                }
                var responseData = JsonSerializer.Deserialize<ProvidersDto>(response);
                if (responseData == null)
                {
                    return new ProvidersDto();
                }
                return responseData;
            }
            catch (Exception e)
            {
                Console.WriteLine("Fecth provide"+ e);
                return new ProvidersDto();
            }
        }

        public async Task<ExchangesDto> FetchDataExchanges()
        {
            try
            {
                string token = await _tokenService.GetValidToken();
                if (string.IsNullOrWhiteSpace(token))
                {
                    return new ExchangesDto();
                }
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetStringAsync("exchanges");
                if (string.IsNullOrWhiteSpace(response))
                {
                    return new ExchangesDto();
                }
                var responseData = JsonSerializer.Deserialize<ExchangesDto>(response);
                if (responseData == null)
                {
                    return new ExchangesDto();
                }
                return responseData;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error  FetchData" + e);
                return new ExchangesDto();
            }
        }
    }
}
