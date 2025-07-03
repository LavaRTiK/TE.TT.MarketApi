using TE.TT.MarketApi.Abstarct;
using TE.TT.MarketApi.Repository;
using TE.TT.MarketApi.Service;

namespace TE.TT.MarketApi.ExtensionMethods
{
    public static class ExtensionAddServices
    {
        public static void AddService(this IServiceCollection service)
        {
            service.AddHttpClient("TokenClient", client =>
            {
                client.BaseAddress = new Uri("https://platform.fintacharts.com/identity/realms/fintatech/protocol/openid-connect/token");
            });
            service.AddHttpClient("ApiClient", client =>
            {
                client.BaseAddress = new Uri("https://platform.fintacharts.com/api/instruments/v1/");
            });
            service.AddSingleton<IControlTokenService,ControlTokenService>();
            service.AddScoped<IAssetRepositoryService, AssetRepositoryService>();
            service.AddSingleton<IFintaApiService,FintaApiService>();
            service.AddHostedService<ScraperDataService>();
        }
    }
}
