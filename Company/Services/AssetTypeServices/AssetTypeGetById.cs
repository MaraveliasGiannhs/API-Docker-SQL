using CompanyWork.Data;
using CompanyWork.Interfaces;
using CompanyWork.Models;
using Microsoft.AspNetCore.Mvc;

namespace CompanyWork.Services.AssetTypeServices
{
    public class AssetTypeGetById(MyDbContext db) : IGetById<AssetTypeDTO>
    {
        private readonly MyDbContext _db = db;

        public async Task<ActionResult<List<AssetTypeDTO>>> GetByIdAsync(Guid id)
        {

            AssetType? assetType = await _db.AssetType.FindAsync(id);

            if (assetType == null)
                throw new NotImplementedException("AssetType is null");

            AssetTypeDTO assetTypeDTO = new()
            {
                Id = assetType.Id,
                Name = assetType.Name,
            };

            List<AssetTypeDTO> assetTypeDTOList = new() { assetTypeDTO };
            return assetTypeDTOList;
        }
    }
}
