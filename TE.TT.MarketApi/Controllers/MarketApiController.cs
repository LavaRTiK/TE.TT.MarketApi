using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TE.TT.MarketApi.Abstarct;
using TE.TT.MarketApi.Database.Entity;
using TE.TT.MarketApi.Model;

namespace TE.TT.MarketApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarketApiController : ControllerBase
    {
        private readonly IAssetRepositoryService _repositoryService;
        private readonly IConvertDtoService _convertDtoService;
        public MarketApiController(IAssetRepositoryService repositoryService, IConvertDtoService convertDtoService)
        {
            _repositoryService = repositoryService;
            _convertDtoService = convertDtoService;
        }
        [HttpGet("instruments")]
        public async Task<ActionResult<IEnumerable<AssetDto>>> Get([FromQuery] bool viewDataUpdate = false, [FromQuery] bool viewMapping = true, [FromQuery] bool viewTrading = true, [FromQuery] bool viewGics = true, [FromQuery] bool viewProfile = true,[FromQuery] string kind = "",
            [FromQuery] string symbol = "", [FromQuery] int size = 10, [FromQuery] int paging = 0)
        {
            var entitys =await _repositoryService.GetListEntity(viewMapping,viewTrading,viewGics,viewProfile,kind,symbol,size,paging);
            if (entitys is null)
            {
                return NotFound("Count Object 0");
            }
            List<AssetDto> listDto = new List<AssetDto>();
            foreach (var entity in entitys)
            {
                var conDto = _convertDtoService.ConvertEntityToDto(entity, viewDataUpdate);
                if (conDto is null)
                {
                    return BadRequest("Error convertDtoObject");
                }
                listDto.Add(conDto);
            }
            //test
            if (listDto.Count == 0)
            {
                NotFound("DtoObject cout 0");
            }
            return listDto;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AssetDto>> GetId(Guid id, [FromQuery] bool viewUpdateData = true)
        {
            var assetEntity = await _repositoryService.GetAssetId(id);
            if (assetEntity == null)
            {
                return NotFound();
            }

            var asstDto = _convertDtoService.ConvertEntityToDto(assetEntity,viewUpdateData);
            if (asstDto is null)
            {
                return BadRequest();
            }

            return asstDto;
        }
        [HttpGet("providers")]
        public async Task<ActionResult<List<ProviderDto>>> GetProviders([FromQuery] bool viewDataUpdate = false)
        {
            var provides = await _repositoryService.GetProviders();
            if (provides == null || provides.Count() == 0)
            {
                return NotFound();
            }
            List<ProviderDto> providersDto = new List<ProviderDto>();
            foreach (var item in provides)
            {
                var provideCon = _convertDtoService.ConvertEnrirtyToDtoProvider(item, viewDataUpdate);
                if (provideCon is null)
                {
                    return BadRequest();
                }
                providersDto.Add(provideCon);

            }
            return providersDto;
        }

        [HttpGet("exchanges")]
        public async Task<ActionResult<ExchangesDto>> GetExchanges([FromQuery] string provide = "")
        {
            var exchgange = await _repositoryService.GetProviderExchangeList(provide);
            if (exchgange is null || exchgange.Count() == 0)
            {
                return NotFound();
            }
            Console.WriteLine("test");
            var exDto = _convertDtoService.ConvertEnitytoDtoProviderExchangeList(exchgange);
            if (exDto is null || exDto.Exchanges.Count == 0)
            {
                return BadRequest();
            }

            return exDto;
        }
    }
}
