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
                await Console.Out.WriteLineAsync("start backservice");
                try
                {
                    using (var scope = _service.CreateScope())
                    {
                        var assetRepository = scope.ServiceProvider.GetRequiredService<IAssetRepositoryService>();
                        var data = await _fintaApiService.FetchAllData();
                        if (data != null && data.ListAssets != null)
                        {
                            await assetRepository.UpdateAssetRepository(data);
                        }
                        else
                        {
                            Console.WriteLine("BackService Data:null");
                        }
                        var dataProvide = await _fintaApiService.FetchDataProviders();
                        if (dataProvide != null || dataProvide.Providers != null || dataProvide.Providers.Count != 0)
                        {
                            var dataExchange = await _fintaApiService.FetchDataExchanges();
                            if (dataExchange != null || dataExchange.Exchanges != null || dataExchange.Exchanges.Count != 0)
                            {
                                await assetRepository.UpdateExchange(dataProvide, dataExchange);
                            }
                            else
                            {
                                Console.WriteLine("Error:scraper null or fiall dataExchange");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Error:scraper null or fail dataProvide");
                        }
                    }
                    await Console.Out.WriteLineAsync("Update Database");
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
