using System.Text.Json.Serialization;

namespace TE.TT.MarketApi.Model
{
    public class TakePagingDto
    {
        [JsonPropertyName("paging")]
        public PagingDto Paging { get; set; }
    }
}
