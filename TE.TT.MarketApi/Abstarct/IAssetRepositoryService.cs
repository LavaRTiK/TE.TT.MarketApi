using TE.TT.MarketApi.Model;

namespace TE.TT.MarketApi.Abstarct
{
    public interface IAssetRepositoryService
    {
        public Task UpdateAssetRepository(AssetsDto assetsDto);
    }
}
