using System.Text.Json.Serialization;

namespace TE.TT.MarketApi.Model
{
    public class GicsDto
    {
        [JsonPropertyName("sectorId")]
        public int SectorId { get; set; }
        [JsonPropertyName("industryGroupId")]
        public int IndustryGrupID { get; set; }
        [JsonPropertyName("industryId")]
        public int IndustryId { get; set; }
        [JsonPropertyName("subIndustryId")]
        public int SubIndustryId { get; set; }
    }
}
