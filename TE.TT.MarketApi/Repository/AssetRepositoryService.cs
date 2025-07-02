using TE.TT.MarketApi.Database;

namespace TE.TT.MarketApi.Repository
{
    public class AssetRepositoryService
    {
        private readonly DataContext _dataContext;
        public AssetRepositoryService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }


    }
}
