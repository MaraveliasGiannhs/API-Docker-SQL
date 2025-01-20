using CompanyWork.Data;
using CompanyWork.Interfaces;
using CompanyWork.Models;
using CompanyWork.PersistClasses;
using Microsoft.EntityFrameworkCore;

namespace CompanyWork.Services.AssetTypeServices
{
    public class AssetTypePostUpdate(MyDbContext db) : IPostUpdate<AssetTypeDTO, AssetTypePersistDTO>
    {
        private readonly MyDbContext _db = db;

        public async Task<List<AssetTypeDTO>> PostUpdateAsync(AssetTypePersistDTO assetTypeDTO)
        {
            if(!assetTypeDTO.Id.HasValue)
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
            else
            {
                AssetType? assetType = await _db.AssetType.FindAsync(assetTypeDTO.Id);

                if (assetType == null)
                    return null;

                assetType.Name = assetTypeDTO.Name;
                await _db.SaveChangesAsync();


                AssetTypeDTO newAssetTypeDTO = new()
                {
                    Id = assetType.Id,
                    Name = assetType.Name,
                };

                List<AssetTypeDTO> assetTypeList = new();
                assetTypeList.Add(newAssetTypeDTO);

                return assetTypeList;
            }
        }
    }
}
