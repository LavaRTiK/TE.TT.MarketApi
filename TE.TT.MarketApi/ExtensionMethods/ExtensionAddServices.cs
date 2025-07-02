using TE.TT.MarketApi.Abstarct;
using TE.TT.MarketApi.Service;

namespace TE.TT.MarketApi.ExtensionMethods
{
    public static class ExtensionAddServices
    {
        public static void AddService(this IServiceCollection service)
        {
            service.AddSingleton<HttpClient>();
            service.AddSingleton<IControlTokenService,ControlTokenService>();
        }
    }
}
