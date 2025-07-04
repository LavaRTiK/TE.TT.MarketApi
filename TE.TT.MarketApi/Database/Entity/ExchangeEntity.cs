namespace TE.TT.MarketApi.Database.Entity
{
    public class ExchangeEntity : BaseEntity
    {
         public string Name { get; set; }
         public Guid ProviderId{ get; set; }
         public Provider Provider{ get; set; }
    }
}
