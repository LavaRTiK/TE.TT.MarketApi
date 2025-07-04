using Microsoft.EntityFrameworkCore;
using TE.TT.MarketApi.Database.Entity;
using TE.TT.MarketApi.Model;

namespace TE.TT.MarketApi.Database
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        public DbSet<AssetEntity> Assets { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<ExchangeEntity> Exchanges { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExchangeEntity>()
                .HasOne(e => e.Provider)
                .WithMany(p => p.Exchanges)
                .HasForeignKey(e => e.ProviderId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<AssetEntity>()
                .HasOne(a => a.Profile)
                .WithOne(p => p.Asset)
                .HasForeignKey<Profile>(p => p.AssetId);

            modelBuilder.Entity<Profile>()
                .HasOne(p => p.Gics)
                .WithOne(g => g.Profile)
                .HasForeignKey<Gics>(g => g.ProfileId);
            //test разные таблицы 

            modelBuilder.Entity<AssetEntity>()
                .HasOne(a => a.Simulation)
                .WithOne(m => m.Asset)
                .HasForeignKey<MappingSimulation>(m => m.AssetId);

            modelBuilder.Entity<AssetEntity >()
                .HasOne(a => a.Alpaca)
                .WithOne(m => m.Asset)
                .HasForeignKey<MappingAlpaca>(m => m.AssetId);

            modelBuilder.Entity<AssetEntity>()
                .HasOne(a => a.Dxfeed)
                .WithOne(m => m.Asset)
                .HasForeignKey<MappingDxfeed>(m => m.AssetId);

            modelBuilder.Entity<AssetEntity>()
                .HasOne(a => a.Oanda)
                .WithOne(m => m.Asset)
                .HasForeignKey<MappingOanda>(m => m.AssetId);

            modelBuilder.Entity<TradingHours>()
                .HasOne(th => th.SimulationMapping)
                .WithOne(m => m.TradingHours)
                .HasForeignKey<TradingHours>(th => th.SimulationMappingId);
            modelBuilder.Entity<TradingHours>()
                .HasOne(th => th.AlpacaMapping)
                .WithOne(m => m.TradingHours)
                .HasForeignKey<TradingHours>(th => th.AlpacaMappingId);
            modelBuilder.Entity<TradingHours>()
                .HasOne(th => th.DxfeedMapping)
                .WithOne(m => m.TradingHours)
                .HasForeignKey<TradingHours>(th => th.DxfeedMappingId);
            modelBuilder.Entity<TradingHours>()
                .HasOne(th => th.OandaMapping)
                .WithOne(m => m.TradingHours)
                .HasForeignKey<TradingHours>(th => th.OandaMappingId);
        }
    }
}
