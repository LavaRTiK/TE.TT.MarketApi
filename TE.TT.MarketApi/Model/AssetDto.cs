using System.Runtime;
using System.Text.Json.Serialization;
using Microsoft.VisualBasic;

namespace TE.TT.MarketApi.Model
{
    public class AssetDto
    {
        [JsonPropertyName("Id")]
        public string? Id { get; set; }
        [JsonPropertyName("symbol")]
        public string? Symbol { get; set; }
        [JsonPropertyName("kind")]
        public string? Kind { get; set; }
        [JsonPropertyName("exchange")]
        public string? Exchange { get; set; }
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        [JsonPropertyName("tickSize")]
        public double? TickSize { get; set; }
        [JsonPropertyName("currency")]
        public string? Currency { get; set; }
        [JsonPropertyName("mappings")]
        public MappingsDto? Mappings { get; set; }
        [JsonPropertyName("profile")]
        public ProfileDto? Profile { get; set; }
        //reverseConvert
        [JsonPropertyName("updateTime")]
        public DateTime? UpdateTime { get; set; }

    }
}
