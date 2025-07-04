namespace TE.TT.MarketApi.Database.Entity
{
    public class Provider : BaseEntity
    {
        public string Name { get; set; }
        public List<ExchangeEntity> Exchanges { get; set; }
    }
}
