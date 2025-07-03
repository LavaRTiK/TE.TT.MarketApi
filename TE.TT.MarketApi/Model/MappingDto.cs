using System.Text.Json.Serialization;

namespace TE.TT.MarketApi.Model
{
    public class MappingDto
    {
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }
        [JsonPropertyName("exchange")]
        public string Exchange { get; set; }
        [JsonPropertyName("defaultOrderSize")]
        public int DefualtOrderSize { get; set; }
        [JsonPropertyName("maxOrderSize")]
        public int MaxOrderSize { get; set; }
        [JsonPropertyName("tradingHours")]
        public TradingHoursDto TradingHours { get; set; }
    }
}
