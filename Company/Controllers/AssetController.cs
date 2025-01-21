using static CompanyWork.Models.AssetDTO;
using CompanyWork.Data;
using Microsoft.AspNetCore.Mvc;
using CompanyWork.Models;
using CompanyWork.Lookup;
using CompanyWork.PersistClasses;
using CompanyWork.Services.AssetServices;


namespace CompanyWork.Controllers
{
    [ApiController]
    [Route("api/assets")]
    public class AssetController : ControllerBase
    {
        private readonly ILogger<AssetController> _logger;
        private readonly MyDbContext _db;

        private readonly AssetPostUpdate _assetPostService;
        private readonly AssetSearch _assetSearchService;
        private readonly AssetDelete _assetDeleteService;
        private readonly AssetGetById _assetGetByIdService;

        public AssetController(
            ILogger<AssetController> logger,
            MyDbContext db,
            AssetPostUpdate assetPostService,
            AssetSearch assetSearchService,
            AssetGetById assetGetById,
            AssetDelete assetDelete
            )
        {
            _logger = logger;
            _db = db;
            _assetPostService = assetPostService;
            _assetSearchService = assetSearchService;
            _assetDeleteService = assetDelete;
            _assetGetByIdService = assetGetById;
        }




        [HttpPost]
        public async Task<List<AssetDTO>> CreateAsset(AssetPersistDTO assetPersistDTO)
        {
            return await _assetPostService.PostUpdateAsync(assetPersistDTO);
        }




        [HttpPost("search")] // + getAll
        public async Task<ActionResult<List<AssetDTO>>> SearchTerm(AssetLookup lookup)
        {
            return await _assetSearchService.SearchTermAsync(lookup);
        }




        [HttpGet("{id}")]
        public async Task<ActionResult<List<AssetDTO>>> ReadAsset(Guid id)
        {
            return await _assetGetByIdService.GetByIdAsync(id);
        }



        [HttpDelete("{id}")]
        public async Task DeleteAsset(Guid id)
        {
            await _assetDeleteService.DeleteAsync(id);
        }
    }
}
