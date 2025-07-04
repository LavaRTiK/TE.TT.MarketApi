using TE.TT.MarketApi.Database.Entity;
using TE.TT.MarketApi.Model;

namespace TE.TT.MarketApi.Abstarct
{
    public interface IAssetRepositoryService
    {
        public Task<AssetEntity> GetAssetId(Guid id);

        public Task<IEnumerable<AssetEntity>> GetListEntity(bool viewMapping, bool viewTrading, bool viewGics,
            bool viewProfile, string kind, string symbol, int size, int paging);
        public Task UpdateAssetRepository(AssetsDto assetsDto);
    }
}
