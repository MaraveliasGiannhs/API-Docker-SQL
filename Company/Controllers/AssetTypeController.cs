using System.Data.Entity;
using Company.Data;
using Company.Models;
using Microsoft.AspNetCore.Mvc;

namespace Company.Controllers
{
    [ApiController]
    [Route("api/assetTypes")] //?
    public class AssetTypeController
    {

        private readonly ILogger<AssetTypeController> _logger;
        private readonly MyDbContext _db;

        public AssetTypeController(ILogger<AssetTypeController> logger, MyDbContext db)
        {
            _logger = logger;
            _db = db;
        }




        [HttpPost("/assetType")] //?
        public async Task<IResult> CreateAsset(AssetTypeDTO assetTypeDTO)
        {

            var assetType = new AssetTypeModel()
            {
                Id = Guid.NewGuid(),
                Name = assetTypeDTO.Name,

            };

            await _db.AssetType.AddAsync(assetType);

            await _db.SaveChangesAsync();

            var newAssetType = new AssetModelDTO()
            {
                Id = assetType.Id,
                Name = assetType.Name
            };

            return TypedResults.Ok(newAssetType);
        }





        [HttpGet("/assetType/{id}")]
        public async Task<IResult> ReadAsset(Guid id)
        {
            AssetTypeModel? assetType;
            assetType = await _db.AssetType.FindAsync(id);

            if (assetType == null)
                return TypedResults.NotFound(assetType);

            var assetTypeDTO = new AssetModelDTO()
            {
                Id = assetType.Id,
                Name = assetType.Name,
              };
            return TypedResults.Ok(assetTypeDTO);
        }




        [HttpGet("/assetType")]
        public async Task<IResult> ReadAllAsset(MyDbContext _db)
        {
            var assetTypeDTO = await _db.AssetType.Select(assetType => new AssetModelDTO()
            {

                Id = assetType.Id, //?
                Name = assetType.Name,

            }).ToListAsync();


            return TypedResults.Ok(assetTypeDTO);

        }





        [HttpPut("/assetType/{id}")]
        public async Task<IResult> UpdateAsset(Guid id, AssetTypeDTO assetTypeDTO)
        {
            var assetType = await _db.AssetType.FindAsync(id);

            if (assetType == null)
                return TypedResults.NotFound(assetType);


            assetType.Name = assetTypeDTO.Name;
       

            var newAssetTypeDTO = new AssetModelDTO()
            {
                Id = assetType.Id,
                Name = assetType.Name,
                
            };

            await _db.SaveChangesAsync();

            return TypedResults.Ok(newAssetTypeDTO);

        }




        [HttpDelete("/assetType/{id}")]
        public async Task<IResult> DeleteAsset(Guid id)
        {
            var assetType = await _db.AssetType.FindAsync(id);

            if (assetType == null)
                return TypedResults.NotFound();

            _db.AssetType.Remove(assetType);
            await _db.SaveChangesAsync();

            return TypedResults.NoContent();

        }
    }
}
