using TE.TT.MarketApi.Database.Entity;
using TE.TT.MarketApi.Model;

namespace TE.TT.MarketApi.Abstarct
{
    public interface IConvertDtoService
    {
        public AssetDto ConvertEntityToDto(AssetEntity entity, bool viewUpdate=true);
        public ProviderDto ConvertEnrirtyToDtoProvider(Provider entity, bool viewDataUpdate);
        public ExchangesDto ConvertEnitytoDtoProviderExchangeList(IEnumerable<Provider> entitys);
    }
}
