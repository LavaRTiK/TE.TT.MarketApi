using System.Text.Json.Serialization;

namespace TE.TT.MarketApi.Model
{
    public class RegisterTokenDto
    {
        [JsonPropertyName("grand_type")]
        public string GrandType { get; set; }
        [JsonPropertyName("client_id")]
        public string ClientId { get; set; }
        [JsonPropertyName("username")]
        public string Username { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
