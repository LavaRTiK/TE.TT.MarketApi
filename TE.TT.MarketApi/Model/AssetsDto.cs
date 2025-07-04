using System.Text.Json;
using System.Text.Json.Serialization;

namespace TE.TT.MarketApi.Model
{
    public class AssetsDto
    {
        [JsonPropertyName("data")]
        public IEnumerable<AssetDto>? ListAssets { get; set; }
    }
}
