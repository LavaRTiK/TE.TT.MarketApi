namespace TE.TT.MarketApi.Database.Entity
{
    public class AssetEntity : BaseEntity
    {
        public string? Symbol { get; set; }
        public string? Kind { get; set; }
        public string? Exchange { get; set; }
        public string? Description { get; set; }
        public double? TickSize { get; set; }
        public string? Currency { get; set; }
        public MappingSimulation? Simulation { get; set; }
        public MappingAlpaca? Alpaca { get; set; }
        public MappingDxfeed? Dxfeed { get; set; }
        public MappingOanda? Oanda { get; set; }
        public Profile? Profile { get; set; }
    }
    public class MappingSimulation : BaseEntity
    {
        public string? Symbol { get; set; }
        public string? Exchange { get; set; }
        public int? DefualtOrderSize { get; set; }
        public int? MaxOrderSize { get; set; }
        public TradingHours? TradingHours { get; set; }
        public Guid? AssetId { get; set; }
        public AssetEntity? Asset { get; set; }
    }

    public class MappingAlpaca : BaseEntity
    {
        public string? Symbol { get; set; }
        public string? Exchange { get; set; }
        public int? DefualtOrderSize { get; set; }
        public int? MaxOrderSize { get; set; }
        public TradingHours? TradingHours { get; set; }
        public Guid? AssetId { get; set; }
        public AssetEntity? Asset { get; set; }
    }

    public class MappingDxfeed : BaseEntity
    {
        public string? Symbol { get; set; }
        public string? Exchange { get; set; }
        public int? DefualtOrderSize { get; set; }
        public int? MaxOrderSize { get; set; }
        public TradingHours? TradingHours { get; set; }
        public Guid? AssetId { get; set; }
        public AssetEntity? Asset { get; set; }
    }

    public class MappingOanda : BaseEntity
    {
        public string? Symbol { get; set; }
        public string? Exchange { get; set; }
        public int? DefualtOrderSize { get; set; }
        public int? MaxOrderSize { get; set; }
        public TradingHours? TradingHours { get; set; }
        public Guid? AssetId { get; set; }
        public AssetEntity? Asset { get; set; }
    }
    public class TradingHours : BaseEntity
    {
        public string? RegularStart { get; set; }
        public string? RegularEnd { get; set; }
        public string? ElectronicStart { get; set; }
        public string? ElectronicEnd { get; set; }
        //test
        public Guid? SimulationMappingId { get; set; }
        public MappingSimulation? SimulationMapping { get; set; }
        public Guid? AlpacaMappingId { get; set; }
        public MappingAlpaca? AlpacaMapping { get; set; }
        public Guid? DxfeedMappingId { get; set; }
        public MappingDxfeed? DxfeedMapping { get; set; }
        public Guid? OandaMappingId { get; set; }
        public MappingOanda? OandaMapping { get; set; }
    }
    public class Profile : BaseEntity
    {
        public string? Name { get; set; }
        public string? Location { get; set; }
        public Gics? Gics { get; set; }
        public Guid? AssetId { get; set; }
        public AssetEntity? Asset { get; set; }
    }
    public class Gics : BaseEntity
    {
        public int? SectorId { get; set; }
        public int? IndustryGroupId { get; set; }
        public int? IndustryId { get; set; }
        public int? SubIndustryId { get; set; }
        public Guid? ProfileId { get; set; }
        public Profile? Profile { get; set; }
    }

}
