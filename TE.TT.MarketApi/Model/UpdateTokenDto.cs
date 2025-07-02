using System.Text.Json.Serialization;

namespace TE.TT.MarketApi.Model
{
    public class UpdateTokenDto
    {
        [JsonPropertyName("grant_type")]
        public string GrantType { get; set; }
        [JsonPropertyName("client_id")]
        public string ClientId { get; set; }
        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }
    }
}
