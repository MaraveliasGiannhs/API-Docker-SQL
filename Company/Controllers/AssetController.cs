using static CompanyWork.Models.AssetDTO;
using CompanyWork.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CompanyWork.Models;
using CompanyWork.Lookup;
using System.Reflection.Metadata.Ecma335;


namespace CompanyWork.Controllers
{
    [ApiController]
    [Route("api/assets")]
    public class AssetController : ControllerBase
    {


        private readonly ILogger<AssetController> _logger;
        private readonly MyDbContext _db;

        public AssetController(ILogger<AssetController> logger, MyDbContext db)
        {
            _logger = logger;
            _db = db;
        }




        [HttpPost]
        public async Task<IResult> CreateAsset(AssetPersistDTO assetPersistDTO)
        {
            if (!assetPersistDTO.Id.HasValue) //post
            {
                Asset asset = new()  //add data to model
                {
                    Id = Guid.NewGuid(),
                    Name = assetPersistDTO.Name,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    AssetTypeId = assetPersistDTO.AssetTypeId,
                };

                await _db.Asset.AddAsync(asset); //add to db

                await _db.SaveChangesAsync();  //save to db

                AssetDTO newAssetDTO = new()  //add to new dto to send back 
                {
                    Id = asset.Id,
                    Name = asset.Name,
                    CreatedAt = asset.CreatedAt,
                    UpdatedAt = asset.UpdatedAt,
                    AssetType =
                    {
                        Id = assetPersistDTO.AssetTypeId,
                        Name = assetPersistDTO.Name
                    }
                }; //Null ref exception

                if (newAssetDTO.AssetType == null)
                    Console.WriteLine("Asset Type Not found");
                return TypedResults.Ok(newAssetDTO);
            }
            else //put
            {
                Asset? asset = await _db.Asset.FindAsync(assetPersistDTO.Id);

                if (asset == null)
                    return TypedResults.NotFound(asset);


                asset.Name = assetPersistDTO.Name;
                //asset.CreatedAt = assetDTO.CreatedAt;
                asset.UpdatedAt = DateTime.UtcNow;
                asset.AssetTypeId = assetPersistDTO.AssetTypeId;

                AssetDTO newAssetDTO = new()
                {
                    Id = asset.Id,
                    Name = asset.Name,
                    CreatedAt = asset.CreatedAt,
                    UpdatedAt = asset.UpdatedAt,
                    AssetType =
                    {
                        Id = assetPersistDTO.AssetTypeId,
                        Name = assetPersistDTO.Name,
                    }
                };

                await _db.SaveChangesAsync();

                return TypedResults.Ok(assetPersistDTO);
            }
        }





        

        [HttpPost("search")] // + getAll
        public async Task<ActionResult<AssetDTO>> SearchTerm(AssetLookup lookup)
        {
            List<Asset> asset = await _db.Asset.ToListAsync();
            List<AssetDTO> assetDTO = await AssetDTO.MapAssets(_db, asset);

            if (lookup == null)
                return BadRequest("Search term cannot be empty.");

            IQueryable<Asset> assetDb = _db.Asset; // all items


            // Filters
            if (!string.IsNullOrWhiteSpace(lookup.Like))
                assetDb = assetDb.Where(a => a.Name.Contains(lookup.Like.ToLower()));

            if (lookup.Id.HasValue)
                assetDb = assetDb.Where(a => a.Id == lookup.Id.Value);


            var searchTerm = await assetDb.ToListAsync();

            return Ok(searchTerm);
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
