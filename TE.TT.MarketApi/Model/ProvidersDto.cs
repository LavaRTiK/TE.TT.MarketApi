using System.Text.Json.Serialization;

namespace TE.TT.MarketApi.Model
{
    public class ProvidersDto
    {
        [JsonPropertyName("data")]
        public List<string> Providers { get; set; }
    }
}
