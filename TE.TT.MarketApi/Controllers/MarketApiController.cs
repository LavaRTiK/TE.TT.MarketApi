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
        public ActionResult<IEnumerable<AssetDto>> Get([FromQuery] bool viewDataUpdate = false, [FromQuery] string kind = "",
            [FromQuery] string symbol = "", [FromQuery] int size = 10, [FromQuery] int paging = 0)
        {
            var entity = _repositoryService.GetListEntity(viewDataUpdate,viewMapping,viewTrading,viewGics,viewProfile,kind,symbol,size,paging);
            return new List<AssetDto>();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<AssetDto>> GetId(Guid id)
        {
            var assetEntity =await _repositoryService.GetAssetId(id);
            if (assetEntity == null)
            {
                return NotFound();
            }
            var asstDto = _convertDtoService.ConvertEntityToDto(assetEntity);
            if (asstDto is null)
            {
                return BadRequest();
            }
            return asstDto;
        }
    }
}
