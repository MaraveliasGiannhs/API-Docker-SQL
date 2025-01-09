using Company.Data;
using Company.Lookup;
using Company.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.Controllers
{
    [ApiController]
    [Route("api/assets")] //?
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
        public async Task<IResult> CreateAsset(AssetModelDTO assetDTO)
        {
            if (!assetDTO.Id.HasValue)
            {
                var asset = new AssetModel()
                {
                    Id = Guid.NewGuid(),
                    Name = assetDTO.Name,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    AssetTypeId = assetDTO.AssetTypeId,
                };

                await _db.Asset.AddAsync(asset);

                await _db.SaveChangesAsync();

                var newAssetDTO = new AssetModelDTO()
                {
                    Id = asset.Id,
                    Name = asset.Name,
                    CreatedAt = asset.CreatedAt,
                    UpdatedAt = asset.UpdatedAt,
                    AssetTypeId = assetDTO.AssetTypeId,
                };

                return TypedResults.Ok(newAssetDTO);
            }
            else
            {
                var asset = await _db.Asset.FindAsync(assetDTO.Id);

                if (asset == null)
                    return TypedResults.NotFound(asset);


                asset.Name = assetDTO.Name;
                //asset.CreatedAt = assetDTO.CreatedAt;
                asset.UpdatedAt = DateTime.UtcNow;
                asset.AssetTypeId = assetDTO.AssetTypeId;

                var newAssetDTO = new AssetModelDTO()
                {
                    Id = asset.Id,
                    Name = asset.Name,
                    CreatedAt = asset.CreatedAt,
                    UpdatedAt = asset.UpdatedAt,
                    AssetTypeId = asset.AssetTypeId,
                };

                await _db.SaveChangesAsync();

                return TypedResults.Ok(assetDTO);
            }
        }





        [HttpGet("{id}")]
        public async Task<IResult> ReadAsset(Guid id)
        {
            AssetModel? asset;
            asset = await _db.Asset.FindAsync(id);

            if (asset == null)
                return TypedResults.NotFound(asset);

            var assetDTO = new AssetModelDTO()
            {
                Id = asset.Id,
                Name = asset.Name,
                CreatedAt = asset.CreatedAt,
                UpdatedAt = asset.UpdatedAt,
                AssetTypeId= asset.AssetTypeId,
            };
            return TypedResults.Ok(assetDTO);
        }




        [HttpGet]
        public async Task<IResult> ReadAllAsset(MyDbContext _db)
        {
            var assetDTO = await _db.Asset.Select(asset => new AssetModelDTO()
            {

                Id = asset.Id, //?
                Name = asset.Name,
                CreatedAt = asset.CreatedAt,
                UpdatedAt = asset.UpdatedAt,
                AssetTypeId= asset.AssetTypeId,
            }).ToListAsync();


            return TypedResults.Ok(assetDTO);

        }



        [HttpPost("search")]
        public async Task<IResult> SearchTerm(AssetLookup lookup)
        {
            if (lookup == null)
                return TypedResults.BadRequest("Search term cannot be empty.");

            IQueryable<AssetModel> assetDb = _db.Asset; //?


            // Filters
            if (!string.IsNullOrWhiteSpace(lookup.Like))
                assetDb = assetDb.Where(a => a.Name.Contains(lookup.Like.ToLower()));

            if (lookup.Id.HasValue)
                assetDb = assetDb.Where(a => a.Id == lookup.Id.Value);


            var searchTerm = await assetDb.ToListAsync();

            return TypedResults.Ok(searchTerm);
        }


        //[HttpPut("{id}")]
        //public async Task<IResult> UpdateAsset(Guid id, AssetModelDTO assetDTO)
        //{
        //    var asset = await _db.Asset.FindAsync(id);

        //    if (asset == null)
        //        return TypedResults.NotFound(asset);


        //    asset.Name = assetDTO.Name;
        //    //asset.CreatedAt = assetDTO.CreatedAt;
        //    asset.UpdatedAt = DateTime.UtcNow;
        //    asset.AssetTypeId = assetDTO.AssetTypeId;

        //    var newAssetDTO = new AssetModelDTO()
        //    {
        //        Id = asset.Id,
        //        Name = asset.Name,
        //        CreatedAt = asset.CreatedAt,
        //        UpdatedAt = asset.UpdatedAt,
        //        AssetTypeId= asset.AssetTypeId,
        //    };

        //    await _db.SaveChangesAsync();

        //    return TypedResults.Ok(assetDTO);

        //}




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
