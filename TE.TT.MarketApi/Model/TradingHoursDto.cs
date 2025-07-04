using System.Text.Json.Serialization;

namespace TE.TT.MarketApi.Model
{
    public class TradingHoursDto
    {
        [JsonPropertyName("regularStart")]
        public string? RegularStart { get; set; }
        [JsonPropertyName("regularEnd")]
        public string? RegularEnd { get; set; }
        [JsonPropertyName("electronicStart")]
        public string? ElectronicStart { get; set; }
        [JsonPropertyName("electronicEnd")]
        public string? ElectronicEnd { get; set; }
    }
}
