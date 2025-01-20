using CompanyWork.Data;
using CompanyWork.Interfaces;
using CompanyWork.Models;
using CompanyWork.PersistClasses;
using Microsoft.EntityFrameworkCore;

namespace CompanyWork.Services.AssetTypeServices
{
    public class AssetTypePost(MyDbContext db) : IPost<AssetTypeDTO, AssetTypePersistDTO>
    {
        private readonly MyDbContext _db = db;

        public async Task<List<AssetTypeDTO>> PostAsync(AssetTypePersistDTO assetTypeDTO)
        {
            AssetType assetType = new()
            {
                Id = Guid.NewGuid(),
                Name = assetTypeDTO.Name,
            };

            await _db.AssetType.AddAsync(assetType);
            await _db.SaveChangesAsync();

            List<AssetType> assetTypeList = await _db.AssetType.ToListAsync();
            List<AssetTypeDTO> newAssetList = new();
            newAssetList = await AssetTypeDTO.MapAssetTypes(_db, assetTypeList);

            
            return newAssetList;
        }
    }
}
