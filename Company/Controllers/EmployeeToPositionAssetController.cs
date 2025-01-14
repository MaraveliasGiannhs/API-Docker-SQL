using Company.Data;
using Company.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.Controllers
{
    [ApiController]
    [Route("api/etpAssets")] //?
    public class EmployeeToPositionAsset : ControllerBase
    {


        private readonly ILogger<EmployeeToPositionAsset> _logger;
        private readonly MyDbContext _db;

        public EmployeeToPositionAsset(ILogger<EmployeeToPositionAsset> logger, MyDbContext db)
        {
            _logger = logger;
            _db = db;
        }




        [HttpPost("/etpAsset")] //?
        public async Task<IResult> CreateEtpAsset(EmployeePositionAssetDTO etpAssetDTO)
        {

            var etpAsset = new EmployeePositionAssetModel()
            {
                Id = Guid.NewGuid(),
                Name = etpAssetDTO.Name,
                EmployeesToPositionId = etpAssetDTO.EmployeesToPositionID,
                AssetId = etpAssetDTO.AssetId,  
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                OwnedAssetFromDateTime = etpAssetDTO.OwnedAssetFromDateTime,
                OwnedAssetTillDateTime = etpAssetDTO.OwnedAssetTillDateTime,

            };

            await _db.EmployeePositionAsset.AddAsync(etpAsset);

            await _db.SaveChangesAsync();

            var newEtpAssetDTO = new EmployeePositionAssetDTO()
            {
                Id = etpAsset.Id,
                Name = etpAsset.Name,
                EmployeesToPositionID=etpAsset.EmployeesToPositionId,
                AssetId=etpAsset.AssetId,
                CreatedAt = etpAsset.CreatedAt,
                UpdatedAt = etpAsset.UpdatedAt,
                OwnedAssetFromDateTime = etpAsset.OwnedAssetFromDateTime,
                OwnedAssetTillDateTime = etpAsset.OwnedAssetTillDateTime,

            };

            return TypedResults.Ok(newEtpAssetDTO);
        }





        [HttpGet("/etpAsset/{id}")]
        public async Task<IResult> ReadEtpAsset(Guid id)
        {
            EmployeePositionAssetModel? etpAsset;
            etpAsset = await _db.EmployeePositionAsset.FindAsync(id);

            if (etpAsset == null)
                return TypedResults.NotFound(etpAsset);

            var etpAssetDTO = new EmployeePositionAssetDTO()
            {
                Id = etpAsset.Id,
                Name = etpAsset.Name,
                EmployeesToPositionID = etpAsset.EmployeesToPositionId,
                AssetId = etpAsset.AssetId,
                CreatedAt = etpAsset.CreatedAt,
                UpdatedAt = etpAsset.UpdatedAt,
                OwnedAssetFromDateTime = etpAsset.OwnedAssetFromDateTime,
                OwnedAssetTillDateTime = etpAsset.OwnedAssetTillDateTime,

            };
            return TypedResults.Ok(etpAssetDTO);
        }




        [HttpGet("/etpAsset")]
        public async Task<IResult> ReadAllEtpAsset(MyDbContext _db)
        {
            var etpAssetDTO = await _db.EmployeePositionAsset.Select(etpAsset => new EmployeePositionAssetDTO()
            {

                Id = etpAsset.Id,
                Name = etpAsset.Name,
                EmployeesToPositionID = etpAsset.EmployeesToPositionId,
                AssetId = etpAsset.AssetId,
                CreatedAt = etpAsset.CreatedAt,
                UpdatedAt = etpAsset.UpdatedAt,
                OwnedAssetFromDateTime = etpAsset.OwnedAssetFromDateTime,
                OwnedAssetTillDateTime = etpAsset.OwnedAssetTillDateTime,

            }).ToListAsync();


            return TypedResults.Ok(etpAssetDTO);

        }





        [HttpPut("/etpAsset/{id}")]
        public async Task<IResult> UpdateEtpAsset(Guid id, EmployeePositionAssetDTO etpAssetDTO)
        {
            var etpAsset = await _db.EmployeePositionAsset.FindAsync(id);

            if (etpAsset== null)
                return TypedResults.NotFound(etpAsset);


            etpAsset.Name = etpAssetDTO.Name;
            etpAsset.EmployeesToPositionId = etpAssetDTO.EmployeesToPositionID;
            etpAsset.AssetId = etpAssetDTO.AssetId; 
            etpAsset.UpdatedAt = etpAssetDTO.UpdatedAt; 
            etpAsset.CreatedAt = etpAssetDTO.CreatedAt;
            etpAsset.OwnedAssetFromDateTime = etpAssetDTO.OwnedAssetFromDateTime;
            etpAsset.OwnedAssetTillDateTime = etpAssetDTO.OwnedAssetTillDateTime;


            var newEtpAssetDTO = new EmployeePositionAssetDTO()
            {
                Id = etpAsset.Id,
                Name = etpAsset.Name,
                EmployeesToPositionID = etpAsset.EmployeesToPositionId,
                AssetId = etpAsset.AssetId, 
                CreatedAt = etpAsset.CreatedAt,
                UpdatedAt = etpAsset.UpdatedAt,
                OwnedAssetFromDateTime = etpAsset.OwnedAssetFromDateTime,
                OwnedAssetTillDateTime = etpAsset.OwnedAssetTillDateTime,

            };

            await _db.SaveChangesAsync();

            return TypedResults.Ok(newEtpAssetDTO);

        }




        [HttpDelete("/etpAsset/{id}")]
        public async Task<IResult> DeleteEtpAsset(Guid id)
        {
            var etpAsset = await _db.EmployeePositionAsset.FindAsync(id);

            if (etpAsset == null)
                return TypedResults.NotFound();

            _db.EmployeePositionAsset.Remove(etpAsset);
            await _db.SaveChangesAsync();

            return TypedResults.NoContent();

        }




    }
}
