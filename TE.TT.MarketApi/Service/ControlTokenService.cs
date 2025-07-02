using System.Globalization;
using System.Text;
using System.Text.Json;
using TE.TT.MarketApi.Abstarct;
using TE.TT.MarketApi.Model;

namespace TE.TT.MarketApi.Service
{
    public class ControlTokenService : IControlTokenService
    {
        private string? _token;
        private string? _refreshToken;
        private DateTime? _tokenExpires;
        private DateTime? _refreshTokenExpires;
        private readonly HttpClient _httpClient;
        private readonly string _username;
        private readonly string _password;
        public ControlTokenService(HttpClient httpClient,IConfiguration configuration)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://platform.fintacharts.com/identity/realms/fintatech/protocol/openid-connect/token");
            _username = configuration.GetValue<string>("username") ?? Environment.GetEnvironmentVariable("USERNAME");
            if (_username is null)
            {
                throw new Exception("user Empty");
            }
            _password = configuration.GetValue<string>("password") ?? Environment.GetEnvironmentVariable("PASSWORD");
            if (_password is null)
            {
                throw new Exception("password Empty");
            }
        }
        public void SetToken(string token, DateTime tokenExpires)
        {
            throw new NotImplementedException();
        }

        public void SetRefreshToken(string refreshToken, DateTime refreshTokenExpires)
        {
            throw new NotImplementedException();
        }

        public void RegisterService(string token, string refreshToken, int tokenExpires, int refreshTokenExpires)
        {
            if (!string.IsNullOrWhiteSpace(token))
            {
                _token = token;
            }

            if (!string.IsNullOrWhiteSpace(refreshToken))
            {
                _refreshToken = refreshToken;
            }

            if (tokenExpires != null)
            {
                _tokenExpires = DateTime.Now.AddSeconds(tokenExpires);
            }

            if (refreshTokenExpires != null)
            {
                _refreshTokenExpires = DateTime.Now.AddSeconds(refreshTokenExpires);
            }
        }

        public string GetToken()
        {
            throw new NotImplementedException();
        }

        public string GetRefeshToken()
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetValidToken()
        {
            if (_tokenExpires <= DateTime.Now)
            {
                if (_refreshTokenExpires <= DateTime.Now)
                {
                    var registerTokenDto = new RegisterTokenDto
                    {
                        GrandType = "password",
                        ClientId = "app-cli",
                        Username = _username,
                        Password = _password
                    };
                    var response = await _httpClient.PostAsJsonAsync("", registerTokenDto);
                    if (!response.IsSuccessStatusCode)
                    {
                        return "";
                    }
                    var content =
                        JsonSerializer.Deserialize<ResponseTokensDto>(await response.Content.ReadAsStringAsync());
                    _token = content.Token;
                    _tokenExpires = DateTime.Now.AddSeconds(content.TokenExpires);
                    _refreshToken = content.RefreshToken;
                    _refreshTokenExpires = DateTime.Now.AddSeconds(content.RefreshTokenExpires);
                }
                else
                {
                    var testObj = new UpdateTokenDto
                    {
                        GrantType = "refresh_token",
                        ClientId = "app-cli",
                        RefreshToken = _refreshToken
                    };
                    var response = await _httpClient.PostAsJsonAsync("", testObj);
                    if (!response.IsSuccessStatusCode)
                    {
                        return "";
                    }
                    var content =
                        JsonSerializer.Deserialize<ResponseTokensDto>(await response.Content.ReadAsStringAsync());
                    _token = content.Token;
                    _tokenExpires = DateTime.Now.AddSeconds(content.TokenExpires);
                    _refreshToken = content.RefreshToken;
                    _refreshTokenExpires = DateTime.Now.AddSeconds(content.RefreshTokenExpires);
                }
            }
            return _token;
        }
    }
}
