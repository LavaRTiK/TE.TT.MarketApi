using System.Text.Json.Serialization;

namespace TE.TT.MarketApi.Model
{
    public class ProviderDto
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("updateTime")]
        public DateTime? UpdateTime { get; set; }
    }
}
