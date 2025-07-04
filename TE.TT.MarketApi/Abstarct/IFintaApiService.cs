using TE.TT.MarketApi.Model;

namespace TE.TT.MarketApi.Abstarct
{
    public interface IFintaApiService
    {
        public Task<AssetsDto> FetchAllData();
        public Task<ProvidersDto> FetchDataProviders();
        public Task<ExchangesDto> FetchDataExchanges();
    }
}
