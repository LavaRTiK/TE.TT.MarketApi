using TE.TT.MarketApi.Database.Entity;
using TE.TT.MarketApi.Model;

namespace TE.TT.MarketApi.Abstarct
{
    public interface IAssetRepositoryService
    {
        public Task<AssetEntity> GetAssetId(Guid id);
        public Task UpdateAssetRepository(AssetsDto assetsDto);
    }
}
