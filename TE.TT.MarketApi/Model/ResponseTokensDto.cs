using System.Text.Json.Serialization;

namespace TE.TT.MarketApi.Model
{
    public class ResponseTokensDto
    {
        [JsonPropertyName("access_token")]
        public string Token { get; set; }
        [JsonPropertyName("expires_in")]
        public int TokenExpires { get; set; }
        [JsonPropertyName("refresh_expires_in")]
        public int RefreshTokenExpires { get; set; }
        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }

    }
}
