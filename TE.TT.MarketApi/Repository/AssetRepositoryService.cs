using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Net.NetworkInformation;
using TE.TT.MarketApi.Abstarct;
using TE.TT.MarketApi.Database;
using TE.TT.MarketApi.Database.Entity;
using TE.TT.MarketApi.Model;

namespace TE.TT.MarketApi.Repository
{
    public class AssetRepositoryService : IAssetRepositoryService
    {
        private readonly DataContext _dataContext;
        public AssetRepositoryService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<AssetEntity> GetAssetId(Guid id)
        {
            var asset = await _dataContext.Assets
                    .Include(x => x.Simulation).ThenInclude(m => m.TradingHours)
                    .Include(x => x.Alpaca).ThenInclude(m => m.TradingHours)
                    .Include(x => x.Dxfeed).ThenInclude(m => m.TradingHours)
                    .Include(x => x.Oanda).ThenInclude(m => m.TradingHours)
                    .Include(x => x.Profile).ThenInclude(p => p.Gics)
                    .FirstOrDefaultAsync(a => a.Id == id);
            return asset;
        }

        public async Task<IEnumerable<AssetEntity>> GetListEntity(bool viewMapping,bool viewTrading,bool viewGics,bool viewProfile,string kind, string symbol,int size,int paging)
        {
            var query = _dataContext.Assets.AsQueryable();
            if (!string.IsNullOrWhiteSpace(kind))
            {
                query = query.Where(x => x.Kind.Contains(kind));
            }

            if (!string.IsNullOrWhiteSpace(symbol))
            {
                query = query.Where(x => x.Symbol.Contains(symbol));
            }

            query = query.Skip(paging)
                .Take(size);
            if (viewMapping)
            {
                if (viewTrading)
                {
                    query = query.Include(x => x.Simulation).ThenInclude(m => m.TradingHours)
                        .Include(x => x.Alpaca).ThenInclude(m => m.TradingHours)
                        .Include(x => x.Dxfeed).ThenInclude(m => m.TradingHours)
                        .Include(x => x.Oanda).ThenInclude(m => m.TradingHours);
                }
                else
                {
                    query = query.Include(x => x.Simulation)
                        .Include(x => x.Alpaca)
                        .Include(x => x.Dxfeed)
                        .Include(x => x.Oanda);
                }
            }

            if (viewProfile)
            {
                if (viewGics)
                {
                    query = query.Include(x => x.Profile).ThenInclude(p => p.Gics);
                }
                else
                {
                    query = query.Include(x => x.Profile);
                }
            }

            var result = await query.ToListAsync();
            return result;
        }
        public async Task UpdateAssetRepository(AssetsDto assetsDto)
        {
            //assset DtoObject 
            //content Ef Object
            foreach (var itemDto in assetsDto.ListAssets)
            {
                var asset = await _dataContext.Assets
                    .Include(x => x.Simulation).ThenInclude(m => m.TradingHours)
                    .Include(x => x.Alpaca).ThenInclude(m => m.TradingHours)
                    .Include(x => x.Dxfeed).ThenInclude(m => m.TradingHours)
                    .Include(x => x.Oanda).ThenInclude(m => m.TradingHours)
                    .Include(x => x.Profile).ThenInclude(p => p.Gics)
                    .FirstOrDefaultAsync(x => x.Symbol == itemDto.Symbol);
                if (asset == null)
                {
                    asset = new AssetEntity
                    {
                        UpdateData = DateTime.Now,
                        Symbol = itemDto.Symbol,
                        Kind = itemDto.Kind,
                        Exchange = itemDto.Exchange,
                        Description = itemDto.Description,
                        TickSize = itemDto.TickSize,
                        Currency = itemDto.Currency,
                        Simulation = itemDto.Mappings?.Simulation != null ? new MappingSimulation
                        {
                            Symbol = itemDto.Mappings.Simulation.Symbol,
                            Exchange = itemDto.Mappings.Simulation.Exchange,
                            DefualtOrderSize = itemDto.Mappings.Simulation.DefualtOrderSize,
                            MaxOrderSize = itemDto.Mappings.Simulation.MaxOrderSize,
                            TradingHours = itemDto.Mappings.Simulation.TradingHours != null ? new TradingHours
                            {
                                RegularStart = itemDto.Mappings.Simulation.TradingHours.RegularStart,
                                RegularEnd = itemDto.Mappings.Simulation.TradingHours.RegularEnd,
                                ElectronicStart = itemDto.Mappings.Simulation.TradingHours.ElectronicStart,
                                ElectronicEnd = itemDto.Mappings.Simulation.TradingHours.ElectronicEnd,
                                UpdateData = DateTime.Now
                            } : null,
                            UpdateData = DateTime.Now
                        } : null,
                        Alpaca = itemDto.Mappings?.Alpaca != null ? new MappingAlpaca()
                        {
                            Symbol = itemDto.Mappings.Alpaca.Symbol,
                            Exchange = itemDto.Mappings.Alpaca.Exchange,
                            DefualtOrderSize = itemDto.Mappings.Alpaca.DefualtOrderSize,
                            MaxOrderSize = itemDto.Mappings.Alpaca.MaxOrderSize,
                            TradingHours = itemDto.Mappings.Alpaca.TradingHours != null ? new TradingHours
                            {
                                RegularStart = itemDto.Mappings.Alpaca.TradingHours.RegularStart,
                                RegularEnd = itemDto.Mappings.Alpaca.TradingHours.RegularEnd,
                                ElectronicStart = itemDto.Mappings.Alpaca.TradingHours.ElectronicStart,
                                ElectronicEnd = itemDto.Mappings.Alpaca.TradingHours.ElectronicEnd,
                                UpdateData = DateTime.Now
                            } : null,
                            UpdateData = DateTime.Now
                        } : null,
                        Dxfeed = itemDto.Mappings?.Dxfeed != null ? new MappingDxfeed()
                        {
                            Symbol = itemDto.Mappings.Dxfeed.Symbol,
                            Exchange = itemDto.Mappings.Dxfeed.Exchange,
                            DefualtOrderSize = itemDto.Mappings.Dxfeed.DefualtOrderSize,
                            MaxOrderSize = itemDto.Mappings.Dxfeed.MaxOrderSize,
                            TradingHours = itemDto.Mappings.Dxfeed.TradingHours != null ? new TradingHours
                            {
                                RegularStart = itemDto.Mappings.Dxfeed.TradingHours.RegularStart,
                                RegularEnd = itemDto.Mappings.Dxfeed.TradingHours.RegularEnd,
                                ElectronicStart = itemDto.Mappings.Dxfeed.TradingHours.ElectronicStart,
                                ElectronicEnd = itemDto.Mappings.Dxfeed.TradingHours.ElectronicEnd,
                                UpdateData = DateTime.Now
                            } : null,
                            UpdateData = DateTime.Now
                        } : null,
                        Oanda = itemDto.Mappings?.Oanda != null ? new MappingOanda()
                        {
                            Symbol = itemDto.Mappings.Oanda.Symbol,
                            Exchange = itemDto.Mappings.Oanda.Exchange,
                            DefualtOrderSize = itemDto.Mappings.Oanda.DefualtOrderSize,
                            MaxOrderSize = itemDto.Mappings.Oanda.MaxOrderSize,
                            TradingHours = itemDto.Mappings.Oanda.TradingHours != null ? new TradingHours
                            {
                                RegularStart = itemDto.Mappings.Oanda.TradingHours.RegularStart,
                                RegularEnd = itemDto.Mappings.Oanda.TradingHours.RegularEnd,
                                ElectronicStart = itemDto.Mappings.Oanda.TradingHours.ElectronicStart,
                                ElectronicEnd = itemDto.Mappings.Oanda.TradingHours.ElectronicEnd,
                                UpdateData = DateTime.Now
                            } : null,
                            UpdateData = DateTime.Now
                        } : null,
                        Profile = itemDto.Profile != null ? new Profile
                        {
                            Name = itemDto.Profile.Name,
                            Location = itemDto.Profile.Location,
                            UpdateData = DateTime.Now,
                            Gics = itemDto.Profile.Gics != null ? new Gics
                            {
                                SectorId = itemDto.Profile.Gics.SectorId,
                                IndustryGroupId = itemDto.Profile.Gics.IndustryGrupID,
                                IndustryId = itemDto.Profile.Gics.IndustryId,
                                SubIndustryId = itemDto.Profile.Gics.SubIndustryId,
                                UpdateData = DateTime.Now,
                            } : null
                        } : null
                    };
                    _dataContext.Assets.Add(asset);
                }
                else
                {
                    asset.UpdateData = DateTime.Now;
                    asset.Kind = itemDto.Kind;
                    asset.Exchange = itemDto.Exchange; 
                    asset.Description = itemDto.Description;
                    asset.TickSize = itemDto.TickSize;
                    asset.Currency = itemDto.Currency;
                    if (itemDto.Mappings.Simulation != null)
                    {
                        if (asset.Simulation == null)
                            asset.Simulation = new MappingSimulation();
                        asset.Simulation.Symbol = itemDto.Mappings.Simulation.Symbol;
                        asset.Simulation.Exchange = itemDto.Mappings.Simulation.Exchange;
                        asset.Simulation.DefualtOrderSize = itemDto.Mappings.Simulation.DefualtOrderSize;
                        asset.UpdateData = DateTime.Now;
                        if (itemDto.Mappings.Simulation.TradingHours != null)
                        {
                            if (asset.Simulation.TradingHours == null)
                                asset.Simulation.TradingHours = new TradingHours();
                            asset.Simulation.TradingHours.RegularStart =
                                itemDto.Mappings.Simulation.TradingHours.RegularStart;
                            asset.Simulation.TradingHours.RegularEnd =
                                itemDto.Mappings.Simulation.TradingHours.RegularEnd;
                            asset.Simulation.TradingHours.ElectronicStart =
                                itemDto.Mappings.Simulation.TradingHours.ElectronicStart;
                            asset.Simulation.TradingHours.ElectronicEnd =
                                itemDto.Mappings.Simulation.TradingHours.ElectronicEnd;
                            asset.UpdateData = DateTime.Now;
                        }
                    }
                    if (itemDto.Mappings.Alpaca != null)
                    {
                        if (asset.Alpaca == null)
                            asset.Alpaca = new MappingAlpaca();
                        asset.Alpaca.Symbol = itemDto.Mappings.Alpaca.Symbol;
                        asset.Alpaca.Exchange = itemDto.Mappings.Alpaca.Exchange;
                        asset.Alpaca.DefualtOrderSize = itemDto.Mappings.Alpaca.DefualtOrderSize;
                        asset.UpdateData = DateTime.Now;
                        if (itemDto.Mappings.Alpaca.TradingHours != null)
                        {
                            if (asset.Alpaca.TradingHours == null)
                                asset.Alpaca.TradingHours = new TradingHours();
                            asset.Alpaca.TradingHours.RegularStart =
                                itemDto.Mappings.Alpaca.TradingHours.RegularStart;
                            asset.Alpaca.TradingHours.RegularEnd =
                                itemDto.Mappings.Alpaca.TradingHours.RegularEnd;
                            asset.Alpaca.TradingHours.ElectronicStart =
                                itemDto.Mappings.Alpaca.TradingHours.ElectronicStart;
                            asset.Alpaca.TradingHours.ElectronicEnd =
                                itemDto.Mappings.Alpaca.TradingHours.ElectronicEnd;
                            asset.UpdateData = DateTime.Now;
                        }
                    }
                    if (itemDto.Mappings.Dxfeed != null)
                    {
                        if (asset.Dxfeed == null)
                            asset.Dxfeed = new MappingDxfeed();
                        asset.Dxfeed.Symbol = itemDto.Mappings.Dxfeed.Symbol;
                        asset.Dxfeed.Exchange = itemDto.Mappings.Dxfeed.Exchange;
                        asset.Dxfeed.DefualtOrderSize = itemDto.Mappings.Dxfeed.DefualtOrderSize;
                        asset.UpdateData = DateTime.Now;
                        if (itemDto.Mappings.Dxfeed.TradingHours != null)
                        {
                            if (asset.Dxfeed.TradingHours == null)
                                asset.Dxfeed.TradingHours = new TradingHours();
                            asset.Dxfeed.TradingHours.RegularStart =
                                itemDto.Mappings.Dxfeed.TradingHours.RegularStart;
                            asset.Dxfeed.TradingHours.RegularEnd =
                                itemDto.Mappings.Dxfeed.TradingHours.RegularEnd;
                            asset.Dxfeed.TradingHours.ElectronicStart =
                                itemDto.Mappings.Dxfeed.TradingHours.ElectronicStart;
                            asset.Dxfeed.TradingHours.ElectronicEnd =
                                itemDto.Mappings.Dxfeed.TradingHours.ElectronicEnd;
                            asset.UpdateData = DateTime.Now;
                        }
                    }
                    if(itemDto.Mappings.Oanda != null)
                    {
                        if (asset.Oanda == null)
                            asset.Oanda = new MappingOanda();
                        asset.Oanda.Symbol = itemDto.Mappings.Oanda.Symbol;
                        asset.Oanda.Exchange = itemDto.Mappings.Oanda.Exchange;
                        asset.Oanda.DefualtOrderSize = itemDto.Mappings.Oanda.DefualtOrderSize;
                        asset.UpdateData = DateTime.Now;
                        if (itemDto.Mappings.Oanda.TradingHours != null)
                        {
                            if (asset.Oanda.TradingHours == null)
                                asset.Oanda.TradingHours = new TradingHours();
                            asset.Oanda.TradingHours.RegularStart =
                                itemDto.Mappings.Oanda.TradingHours.RegularStart;
                            asset.Oanda.TradingHours.RegularEnd =
                                itemDto.Mappings.Oanda.TradingHours.RegularEnd;
                            asset.Oanda.TradingHours.ElectronicStart =
                                itemDto.Mappings.Oanda.TradingHours.ElectronicStart;
                            asset.Oanda.TradingHours.ElectronicEnd =
                                itemDto.Mappings.Oanda.TradingHours.ElectronicEnd;
                            asset.UpdateData = DateTime.Now;
                        }
                    }

                    if (itemDto.Profile != null)
                    {
                        if(asset.Profile == null)
                            asset.Profile = new Profile();
                        asset.Profile.Name = itemDto.Profile.Name;
                        asset.Profile.Location = itemDto.Profile.Location;
                        asset.Profile.UpdateData = DateTime.Now;
                        if (itemDto.Profile.Gics != null)
                        {
                            if (asset.Profile.Gics == null)
                                asset.Profile.Gics = new Gics();
                            asset.Profile.Gics.SectorId = itemDto.Profile.Gics.SectorId;
                            asset.Profile.Gics.IndustryGroupId = itemDto.Profile.Gics.IndustryGrupID;
                            asset.Profile.Gics.IndustryId = itemDto.Profile.Gics.IndustryId;
                            asset.Profile.Gics.SubIndustryId = itemDto.Profile.Gics.SubIndustryId;
                            asset.Profile.UpdateData = DateTime.Now;
                        }
                    }
                }
            }
            await _dataContext.SaveChangesAsync();
        }

    }
}
