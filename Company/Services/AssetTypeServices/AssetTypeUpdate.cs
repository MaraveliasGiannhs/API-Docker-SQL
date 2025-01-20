using CompanyWork.Data;
using CompanyWork.Interfaces;
using CompanyWork.Models;
using CompanyWork.PersistClasses;

namespace CompanyWork.Services.AssetTypeServices
{
    public class AssetTypeUpdate(MyDbContext db) : IUpdate<AssetTypeDTO, AssetTypePersistDTO>
    {
        private readonly MyDbContext _db = db;
        public async Task<List<AssetTypeDTO>> UpdateAsync(AssetTypePersistDTO assetTypePersist)
        {
            AssetType? assetType = await _db.AssetType.FindAsync(assetTypePersist.Id);

            if (assetType == null)
                return null;

            assetType.Name = assetTypePersist.Name;
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
