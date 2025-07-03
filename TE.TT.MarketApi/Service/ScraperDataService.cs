using TE.TT.MarketApi.Abstarct;

namespace TE.TT.MarketApi.Service
{
    public class ScraperDataService : BackgroundService
    {
        private readonly IServiceProvider _service;
        private readonly IFintaApiService _fintaApiService;
        public ScraperDataService(IServiceProvider? service,IFintaApiService fintaApi)
        {
            _service = service;
            _fintaApiService = fintaApi;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //update database
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _service.CreateScope())
                    {
                        var assetRepository = scope.ServiceProvider.GetRequiredService<IAssetRepositoryService>();
                        var data = await _fintaApiService.FetchAllData();
                        await assetRepository.UpdateAssetRepository(data);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
        }
    }
}
