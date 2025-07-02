using Microsoft.EntityFrameworkCore;
using TE.TT.MarketApi.Database.Entity;
using TE.TT.MarketApi.Model;

namespace TE.TT.MarketApi.Database
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        public DbSet<AssetEntity> Assets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AssetEntity>()
                .HasOne(a => a.Profile)
                .WithOne(p => p.Asset)
                .HasForeignKey<Profile>(p => p.AssetId);

            modelBuilder.Entity<Profile>()
                .HasOne(p => p.Gics)
                .WithOne(g => g.Profile)
                .HasForeignKey<Gics>(g => g.ProfileId);

            // Маппинги — ранее описано
            modelBuilder.Entity<AssetEntity>()
                .HasOne(a => a.Simulation)
                .WithOne(m => m.Asset)
                .HasForeignKey<MappingSimulation>(m => m.AssetId);

            modelBuilder.Entity<AssetEntity >()
                .HasOne(a => a.Aplace)
                .WithOne(m => m.Asset)
                .HasForeignKey<MappingAplace>(m => m.AssetId);

            modelBuilder.Entity<AssetEntity>()
                .HasOne(a => a.Dxfeed)
                .WithOne(m => m.Asset)
                .HasForeignKey<MappingDxfeed>(m => m.AssetId);

            modelBuilder.Entity<AssetEntity>()
                .HasOne(a => a.Oanda)
                .WithOne(m => m.Asset)
                .HasForeignKey<MappingOanda>(m => m.AssetId);

            modelBuilder.Entity<TradingHours>()
                .HasOne(th => th.Mapping)
                .WithOne()
                .HasForeignKey<TradingHours>(th => th.MappingId);
        }
    }
}
