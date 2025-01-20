using CompanyWork.Data;
using CompanyWork.Interfaces;
using CompanyWork.Models;
using CompanyWork.PersistClasses;

namespace CompanyWork.Services.AssetServices
{
    public class AssetUpdate(MyDbContext db) : IUpdate<AssetDTO,AssetPersistDTO>
    {
        readonly MyDbContext _db = db;

        public async Task<List<AssetDTO>> UpdateAsync(AssetPersistDTO assetPersistDTO)
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
            return assetDTO;
        }
    }
}
