using TE.TT.MarketApi.Abstarct;

namespace TE.TT.MarketApi.Service
{
    public class ScraperDataService : BackgroundService
    {
        private readonly IServiceProvider _service;
        public ScraperDataService(IServiceProvider service)
        {
            _service = service;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //update database
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _service.CreateScope())
                {
                    var assetRepository = scope.ServiceProvider.GetRequiredService<IAssetRepositoryService>();
                    //даные стянуть через api 
                    //после в репозитори сохранить их
                }
            }
            throw new NotImplementedException();
        }
    }
}
