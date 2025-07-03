using System.ComponentModel.DataAnnotations;

namespace TE.TT.MarketApi.Database.Entity
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime UpdateData { get; set; }
    }
}
