namespace TE.TT.MarketApi.Abstarct
{
    public interface IControlTokenService
    {
        public void SetToken(string token, DateTime tokenExpires);
        public void SetRefreshToken(string refreshToken, DateTime refreshTokenExpires);
        public void RegisterService(string token, string refreshToken, int tokenExpires, int refreshTokenExpires);
        public string GetToken();
        public string GetRefeshToken();
        public Task<string> GetValidToken();
    }
}
