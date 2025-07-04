using System.Text.Json.Serialization;
using TE.TT.MarketApi.Database.Entity;

namespace TE.TT.MarketApi.Model
{
    public class ExchangesDto
    {
        [JsonPropertyName("data")]
        public Dictionary<string, List<string>> Exchanges { get; set; }
    }
}
