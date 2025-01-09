using System.Diagnostics;
using Company.Data;
using Company.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Company.Lookup;




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



        [HttpPost]
        public async Task<IResult> CreateAsset(AssetTypeDTO assetTypeDTO)
        {
            //Trace.WriteLine(assetTypeDTO.Id);

            if (!assetTypeDTO.Id.HasValue) //create
            {
 
                var assetType = new AssetTypeModel()
                {
                    Id = Guid.NewGuid(),
                    Name = assetTypeDTO.Name,

                };

                await _db.AssetType.AddAsync(assetType);
                await _db.SaveChangesAsync();

                var newAssetType = new AssetTypeDTO()
                {
                    Id = assetType.Id,
                    Name = assetType.Name
                };

                return TypedResults.Ok(newAssetType);
            }
            else //update
            {

                var assetType = await _db.AssetType.FindAsync(assetTypeDTO.Id);

                if (assetType == null)
                    return TypedResults.NotFound(assetType);


                assetType.Name = assetTypeDTO.Name;
                await _db.SaveChangesAsync();


                var newAssetTypeDTO = new AssetTypeDTO()
                {
                    Id = assetType.Id,
                    Name = assetType.Name,

                };

 
                return TypedResults.Ok(newAssetTypeDTO);
            }

        }





        [HttpGet("{id}")]
        public async Task<IResult> ReadAsset(Guid id)
        {
            AssetTypeModel? assetType;
            assetType = await _db.AssetType.FindAsync(id);

            if (assetType == null)
                return TypedResults.NotFound(assetType);
            var assetTypeDTO = new AssetTypeDTO()
            {
                Id = assetType.Id,
                Name = assetType.Name,
              };
            return TypedResults.Ok(assetTypeDTO);
        }
        
        ////////////////////SEARCH////////////////////
        [HttpPost("search")]
        public async Task<IResult> SearchTerm(AssetTypeLookup lookup)
        {
            if (lookup == null)
                return TypedResults.BadRequest("Search term cannot be empty.");

            IQueryable<AssetTypeModel> assetTypeDb = _db.AssetType; //?


            // Filters
            if (!string.IsNullOrWhiteSpace(lookup.Like))
                assetTypeDb = assetTypeDb.Where(a => a.Name.Contains(lookup.Like.ToLower()));

            if (lookup.Id.HasValue)
                assetTypeDb = assetTypeDb.Where(a => a.Id == lookup.Id.Value);
                

    
            

            var searchTerm = await assetTypeDb.ToListAsync(); 

            return TypedResults.Ok(searchTerm);
        }


        [HttpGet]
        public async Task<IResult> ReadAllAsset(MyDbContext _db)
        {
            var assetTypeDTO = await _db.AssetType.Select(assetType => new AssetTypeDTO()
            {

                Id = assetType.Id, //?
                Name = assetType.Name,

            }).ToListAsync();


            return TypedResults.Ok(assetTypeDTO);

        }





        //[HttpPut("{id}")]
        //public async Task<IResult> UpdateAsset(Guid id, AssetTypeDTO assetTypeDTO)
        //{
        //    var assetType = await _db.AssetType.FindAsync(id);

        //    if (assetType == null)
        //        return TypedResults.NotFound(assetType);


        //    assetType.Name = assetTypeDTO.Name;
       

        //    var newAssetTypeDTO = new AssetTypeDTO()
        //    {
        //        Id = assetType.Id,
        //        Name = assetType.Name,
                
        //    };

        //    await _db.SaveChangesAsync();

        //    return TypedResults.Ok(newAssetTypeDTO);

        //}




        [HttpDelete("{id}")]
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
