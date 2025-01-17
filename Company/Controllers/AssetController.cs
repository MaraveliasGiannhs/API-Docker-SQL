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
        public AssetController(ILogger<AssetController> logger,MyDbContext db,AssetPost assetPostService)
        {
            _logger = logger;
            _db = db;
            _assetPostService = assetPostService;
        }




        [HttpPost]
        public async Task<List<AssetDTO>> CreateAsset(AssetPersistDTO assetPersistDTO)
        {
            if (!assetPersistDTO.Id.HasValue) //post
            {

                return await _assetPostService.GetAssetsAsync(assetPersistDTO);
                //Asset asset = new()  //add data to model
                //{
                //    Id = Guid.NewGuid(),
                //    Name = assetPersistDTO.Name,
                //    CreatedAt = DateTime.UtcNow,
                //    UpdatedAt = DateTime.UtcNow,
                //    AssetTypeId = assetPersistDTO.AssetTypeId,
                //};

                //await _db.Asset.AddAsync(asset); //add to db
                //await _db.SaveChangesAsync();  //save to db

                //List<Asset> assetList = await _db.Asset.ToListAsync();
                //List<AssetDTO> assetDTO = await AssetDTO.MapAssets(_db, assetList);


                //return assetDTO == null ? throw new InvalidOperationException("No AssetDTO found.") : assetDTO;
                
                //var a = await _assetPostService.GetAssetsAsync(assetPersistDTO);
                //return a;
            }
            else //put
            {
                Asset? assetDb = await _db.Asset.FindAsync(assetPersistDTO.Id);

                if (assetDb == null)
                    throw new InvalidOperationException("No Assets found in DB");

                assetDb.Name = assetPersistDTO.Name;
                assetDb.UpdatedAt = DateTime.UtcNow;
                assetDb.AssetTypeId = assetPersistDTO.AssetTypeId;

 

                List<Asset> assetDtoList = new();
                assetDtoList.Add(assetDb);

                List<AssetDTO> assetDTO = await AssetDTO.MapAssets(_db, assetDtoList);

                await _db.SaveChangesAsync();
                //AssetDTO newAssetDTO = new()
                //{
                //    Id = asset.Id,
                //    Name = asset.Name,
                //    CreatedAt = asset.CreatedAt,
                //    UpdatedAt = asset.UpdatedAt,
                //    AssetType =
                //    {
                //        Id = asset.AssetTypeId,
                //        Name = assetPersistDTO.Name,
                //    }
                //};


                return assetDTO;
            }
        }




        [HttpPost("search")] // + getAll
        public async Task<ActionResult<List<AssetDTO>>> SearchTerm(AssetLookup lookup)
        {
            if (lookup == null)
                return BadRequest("Search term cannot be empty.");

            IQueryable<Asset> assetDb = _db.Asset; // all items


            // Filters
            if (!string.IsNullOrWhiteSpace(lookup.Like))
                assetDb = assetDb.Where(a => a.Name.Contains(lookup.Like.ToLower()));

            if (lookup.Id.HasValue)
                assetDb = assetDb.Where(a => a.Id == lookup.Id.Value);


            List<Asset> assetList = await assetDb.ToListAsync();
            List<AssetDTO> assetDTO = await AssetDTO.MapAssets(_db, assetList);

            return assetDTO;
        }




        [HttpGet("{id}")]
        public async Task<ActionResult<List<AssetDTO>>> ReadAsset(Guid id)
        {
            var asset = await _db.Asset.FindAsync(id);

            if (asset == null)
                return BadRequest("Asset doesnt exist.");

            List<AssetDTO> assetDTO = await MapAssets(_db, new List<Asset> { asset });

            return assetDTO;
        }



        [HttpDelete("{id}")]
        public async Task<IResult> DeleteAsset(Guid id)
        {
            var asset = await _db.Asset.FindAsync(id);

            if (asset == null)
                return TypedResults.NotFound();

            _db.Asset.Remove(asset);
            await _db.SaveChangesAsync();

            return TypedResults.NoContent();

        }
    }
}
