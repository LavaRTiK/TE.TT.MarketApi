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
        private HttpClient _httpClient;
        private readonly string _username;
        private readonly string _password;
        public  ControlTokenService(IHttpClientFactory httpClientFactory,IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient("TokenClient");
            _username = configuration.GetValue<string>("login") ?? Environment.GetEnvironmentVariable("LOGIN");
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

        public async Task FirstStart()
        {
            try
            {
                var requstData = new Dictionary<string, string>()
                {
                    {"grant_type","password"},
                    {"client_id","app-cli"},
                    {"username",_username},
                    {"password",_password}
                };
                var contentData = new FormUrlEncodedContent(requstData);
                //var registerTokenDto = new RegisterTokenDto
                //{
                //    GrandType = "password",
                //    ClientId = "app-cli",
                //    Username = _username,
                //    Password = _password
                //};
                //var response = await _httpClient.PostAsJsonAsync("", registerTokenDto);
                var response = await _httpClient.PostAsync("", contentData);
                if (!response.IsSuccessStatusCode)
                {
                    var errorBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("isSuccess"+errorBody + "fail:" + response.StatusCode);
                    return;
                }
                var content = JsonSerializer.Deserialize<ResponseTokensDto>(await response.Content.ReadAsStringAsync());
                if (content is null)
                {
                    return;
                }
                _token = content.Token;
                _tokenExpires = DateTime.Now.AddSeconds(content.TokenExpires);
                _refreshToken = content.RefreshToken;
                _refreshTokenExpires = DateTime.Now.AddSeconds(content.RefreshTokenExpires);
            }
            catch (Exception e)
            {
                Console.WriteLine("password:" + _password);
                Console.WriteLine("username:" + _username);
                Console.WriteLine("Error FirstStart:"+e);
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
            if (string.IsNullOrWhiteSpace(_token) || _tokenExpires == null)
            {
                await FirstStart();
                if (string.IsNullOrWhiteSpace(_token))
                {
                    return "";
                }
                return _token!;
            }
            if (_tokenExpires <= DateTime.Now)
            {
                if (_refreshTokenExpires <= DateTime.Now)
                {
                    try
                    {
                        var requstData = new Dictionary<string, string>()
                        {
                            {"grant_type","password"},
                            {"client_id","app-cli"},
                            {"username",_username},
                            {"password",_password}
                        };
                        var contentData = new FormUrlEncodedContent(requstData);
                        var response = await _httpClient.PostAsync("", contentData);
                        if (!response.IsSuccessStatusCode)
                        {
                            Console.WriteLine($"refresh token update error:{response.StatusCode}:" + await response.Content.ReadAsStringAsync());
                            return "";
                        }
                        var content =
                            JsonSerializer.Deserialize<ResponseTokensDto>(await response.Content.ReadAsStringAsync());
                        _token = content.Token;
                        _tokenExpires = DateTime.Now.AddSeconds(content.TokenExpires);
                        _refreshToken = content.RefreshToken;
                        _refreshTokenExpires = DateTime.Now.AddSeconds(content.RefreshTokenExpires);
                        await Console.Out.WriteLineAsync("Token:Update");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error refresh Token Update:" + ex);
                        return "";
                    }
                }
                else
                {
                    try
                    {
                        //var testObj = new UpdateTokenDto
                        //{
                        //    GrantType = "refresh_token",
                        //    ClientId = "app-cli",
                        //    RefreshToken = _refreshToken!
                        //};
                        var requstData = new Dictionary<string, string>()
                        {
                            {"grant_type","refresh_token"},
                            {"client_id","app-cli"},
                            {"refresh_token",_refreshToken}

                        };
                        var contentData = new FormUrlEncodedContent(requstData);
                        var response = await _httpClient.PostAsync("", contentData);
                        if (!response.IsSuccessStatusCode)
                        {
                            Console.WriteLine($"token update error:{response.StatusCode}:" + await response.Content.ReadAsStringAsync());
                            return "";
                        }
                        var content =
                            JsonSerializer.Deserialize<ResponseTokensDto>(await response.Content.ReadAsStringAsync());
                        _token = content!.Token;
                        _tokenExpires = DateTime.Now.AddSeconds(content.TokenExpires);
                        _refreshToken = content.RefreshToken;
                        _refreshTokenExpires = DateTime.Now.AddSeconds(content.RefreshTokenExpires);
                        await Console.Out.WriteLineAsync("Token:RefreshUpdate");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error Update Token"+ex);
                    }
                }
            }
            return _token!;
        }
    }
}
