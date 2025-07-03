using System.Text.Json.Serialization;

namespace TE.TT.MarketApi.Model
{
    public class MappingsDto
    {
        [JsonPropertyName("simulation")]
        public MappingDto Simulation { get; set; }
        [JsonPropertyName("alpaca")]
        public MappingDto Alpaca { get; set; }
        [JsonPropertyName("dxfeed")]
        public MappingDto Dxfeed { get; set; }
        [JsonPropertyName("oanda")]
        public MappingDto Oanda { get; set; }
    }
}
