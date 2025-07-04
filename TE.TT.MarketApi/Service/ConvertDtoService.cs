using System.Reflection.Metadata.Ecma335;
using TE.TT.MarketApi.Abstarct;
using TE.TT.MarketApi.Database.Entity;
using TE.TT.MarketApi.Model;

namespace TE.TT.MarketApi.Service
{
    public class ConvertDtoService : IConvertDtoService
    {
        public ConvertDtoService()
        {
        }

        public AssetEntity ConvertDtoToEntity(AssetDto assetDto)
        {
            return new AssetEntity();
        }

        public AssetDto ConvertEntityToDto(AssetEntity entity, bool viewUpdate = true)
        {
            if (entity is null)
            {
                throw new Exception("Convert null entity");
            }
            AssetDto asset = new AssetDto
            {
                Id = entity.Id.ToString(),
                Symbol = entity.Symbol,
                Kind = entity.Kind,
                Exchange = entity.Exchange,
                Description = entity.Description,
                TickSize = entity.TickSize,
                Currency = entity.Currency,
                UpdateTime = viewUpdate ? entity.UpdateData : null,
                Mappings = new MappingsDto
                {
                    Simulation = entity.Simulation != null
                        ? new MappingDto
                        {
                            Symbol = entity.Simulation.Symbol,
                            Exchange = entity.Simulation.Exchange,
                            DefualtOrderSize = entity.Simulation.DefualtOrderSize,
                            MaxOrderSize = entity.Simulation.MaxOrderSize,
                            TradingHours = entity.Simulation.TradingHours != null
                                ? new TradingHoursDto
                                {
                                    RegularStart = entity.Simulation.TradingHours.RegularStart,
                                    RegularEnd = entity.Simulation.TradingHours.RegularEnd,
                                    ElectronicStart = entity.Simulation.TradingHours.ElectronicStart,
                                    ElectronicEnd = entity.Simulation.TradingHours.ElectronicEnd
                                }
                                : null
                        }
                        : null,
                    Alpaca = entity.Alpaca != null
                        ? new MappingDto
                        {
                            Symbol = entity.Alpaca.Symbol,
                            Exchange = entity.Alpaca.Exchange,
                            DefualtOrderSize = entity.Alpaca.DefualtOrderSize,
                            MaxOrderSize = entity.Alpaca.MaxOrderSize,
                            TradingHours = entity.Alpaca.TradingHours != null
                                ? new TradingHoursDto
                                {
                                    RegularStart = entity.Alpaca.TradingHours.RegularStart,
                                    RegularEnd = entity.Alpaca.TradingHours.RegularEnd,
                                    ElectronicStart = entity.Alpaca.TradingHours.ElectronicStart,
                                    ElectronicEnd = entity.Alpaca.TradingHours.ElectronicEnd
                                }
                                : null
                        }
                        : null,
                    Dxfeed = entity.Dxfeed != null
                        ? new MappingDto
                        {
                            Symbol = entity.Dxfeed.Symbol,
                            Exchange = entity.Dxfeed.Exchange,
                            DefualtOrderSize = entity.Dxfeed.DefualtOrderSize,
                            MaxOrderSize = entity.Dxfeed.MaxOrderSize,
                            TradingHours = entity.Dxfeed.TradingHours != null
                                ? new TradingHoursDto
                                {
                                    RegularStart = entity.Dxfeed.TradingHours.RegularStart,
                                    RegularEnd = entity.Dxfeed.TradingHours.RegularEnd,
                                    ElectronicStart = entity.Dxfeed.TradingHours.ElectronicStart,
                                    ElectronicEnd = entity.Dxfeed.TradingHours.ElectronicEnd ?? null
                                }
                                : null
                        }
                        : null,
                    Oanda = entity.Oanda != null
                        ? new MappingDto
                        {
                            Symbol = entity.Oanda.Symbol,
                            Exchange = entity.Oanda.Exchange,
                            DefualtOrderSize = entity.Oanda.DefualtOrderSize,
                            MaxOrderSize = entity.Oanda.MaxOrderSize,
                            TradingHours = entity.Oanda.TradingHours != null
                                ? new TradingHoursDto
                                {
                                    RegularStart = entity.Oanda.TradingHours.RegularStart,
                                    RegularEnd = entity.Oanda.TradingHours.RegularEnd,
                                    ElectronicStart = entity.Oanda.TradingHours.ElectronicStart,
                                    ElectronicEnd = entity.Oanda.TradingHours.ElectronicEnd
                                }
                                : null
                        }
                        : null
                },
                Profile = entity.Profile != null
                    ? new ProfileDto
                    {
                        Name = entity.Profile.Name,
                        Location = entity.Profile.Location,
                        Gics = entity.Profile.Gics != null
                            ? new GicsDto
                            {
                                SectorId = entity.Profile.Gics.SectorId,
                                IndustryGrupID = entity.Profile.Gics.IndustryGroupId,
                                IndustryId = entity.Profile.Gics.IndustryId,
                                SubIndustryId = entity.Profile.Gics.SubIndustryId
                            }
                            : null
                    }
                    : null
            };
            return asset;
        }

    }
}
