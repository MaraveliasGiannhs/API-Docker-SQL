using static CompanyWork.Models.AssetDTO;
using CompanyWork.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CompanyWork.Models;
using CompanyWork.Lookup;
using CompanyWork.Services;
using CompanyWork.Interfaces;


namespace CompanyWork.Controllers
{
    [ApiController]
    [Route("api/assets")]
    public class AssetController : ControllerBase
    {
        private readonly ILogger<AssetController> _logger;
        private readonly MyDbContext _db;

        private readonly AssetPost _assetPostService;
        private readonly AssetUpdate _assetUpdateService;
        private readonly AssetSearch _assetSearchService;
        private readonly AssetDelete _assetDeleteService;
        private readonly AssetGetById _assetGetByIdService;

        public AssetController(
            ILogger<AssetController> logger,
            MyDbContext db,
            AssetPost assetPostService,
            AssetUpdate assetUpdateService,
            AssetSearch assetSearchService,
            AssetGetById assetGetById,
            AssetDelete assetDelete)
        {
            _logger = logger;
            _db = db;
            _assetPostService = assetPostService;
            _assetUpdateService = assetUpdateService;
            _assetSearchService = assetSearchService;
            _assetDeleteService = assetDelete;
            _assetGetByIdService = assetGetById;
        }




        [HttpPost]
        public async Task<List<AssetDTO>> CreateAsset(AssetPersistDTO assetPersistDTO)
        {
            if (!assetPersistDTO.Id.HasValue) //post
            {
                return await _assetPostService.PostAssetsAsync(assetPersistDTO);   
            }
            else //put
            {
                return await _assetUpdateService.UpdateAsset(assetPersistDTO);
            }
        }




        [HttpPost("search")] // + getAll
        public async Task<ActionResult<List<AssetDTO>>> SearchTerm(AssetLookup lookup)
        {
            return await _assetSearchService.SearchTerm(lookup);
        }




        [HttpGet("{id}")]
        public async Task<ActionResult<List<AssetDTO>>> ReadAsset(Guid id)
        {
            return await _assetGetByIdService.ReadAsset(id);
        }



        [HttpDelete("{id}")]
        public async Task<IResult> DeleteAsset(Guid id)
        {
            return await _assetDeleteService.DeleteAsset(id);
        }
    }
}
