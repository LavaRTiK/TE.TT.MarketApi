using System.Runtime;

namespace TE.TT.MarketApi.Model
{
    public class AssetDto
    {
        public string Id { get; set; }
        public string Symbol { get; set; }
        public string Kind { get; set; }
        public string Exchange { get; set; }
        public string Description { get; set; }
        public double TickSize { get; set; }
        public string Currency { get; set; }
        public MappingsDto Mappings { get; set; }
        public ProfileDto Profile { get; set; }

    }
}
