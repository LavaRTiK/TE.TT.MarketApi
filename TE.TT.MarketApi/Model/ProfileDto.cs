using System.Text.Json.Serialization;

namespace TE.TT.MarketApi.Model
{
    public class ProfileDto
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("location")]
        public string? Location { get; set; }
        [JsonPropertyName("gics")]
        public GicsDto? Gics { get; set; }
    }
}
