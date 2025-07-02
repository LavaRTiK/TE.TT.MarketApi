namespace TE.TT.MarketApi.Model
{
    public class MappingDto
    {
        public string Symbol { get; set; }
        public string Exchange { get; set; }
        public int DefualtOrderSize { get; set; }
        public int MaxOrderSize { get; set; }
        public TradingHoursDto TradingHours { get; set; }
    }
}
