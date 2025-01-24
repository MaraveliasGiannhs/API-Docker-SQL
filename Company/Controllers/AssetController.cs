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




        public AssetController(
            ILogger<AssetController> logger,
            MyDbContext db,
            AssetPostUpdate assetPostService,
            AssetSearch assetSearchService,
            AssetDelete assetDelete
            )
        {
            _logger = logger;
            _db = db;
            _assetPostService = assetPostService;
            _assetSearchService = assetSearchService;
            _assetDeleteService = assetDelete;
        }




        [HttpPost]
        public async Task<List<AssetDTO>> CreateAsset(AssetPersistDTO assetPersistDTO)
        {
            return await _assetPostService.PostUpdateAsync(assetPersistDTO);
        }




        [HttpPost("search")]
        public async Task<ActionResult<List<AssetDTO>>> SearchTerm(AssetLookup lookup)
        {
            if (lookup.Id.HasValue)
                _assetSearchService.Ids(lookup.Id.Value);

            if (!string.IsNullOrEmpty(lookup.Like))
                _assetSearchService.Names(lookup.Like);

            if (lookup.PageIndex.HasValue)
                _assetSearchService.PageIndex(lookup.PageIndex.Value);

            if (lookup.ItemsPerPage.HasValue)
                _assetSearchService.PageSize(lookup.ItemsPerPage.Value);

            if (!string.IsNullOrEmpty(lookup.OrderItem))
                _assetSearchService.OrderBy(lookup.OrderItem);

            if (lookup.AscendingOrder.HasValue)
                _assetSearchService.Ascending(lookup.AscendingOrder.Value);

            return await _assetSearchService.SearchTermAsync(lookup);
        }


        [HttpGet]
        public async Task<int> GetElementSum()
        {
            return await _assetSearchService.Count();
        }




        [HttpDelete("{id}")]
        public async Task DeleteAsset(Guid id)
        {
            await _assetDeleteService.DeleteAsync(id);
        }
    }
}
