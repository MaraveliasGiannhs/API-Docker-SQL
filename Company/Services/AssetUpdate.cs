using CompanyWork.Data;
using CompanyWork.Interfaces;
using CompanyWork.Models;

namespace CompanyWork.Services
{
    public class AssetUpdate(MyDbContext _db) : IUpdate
    {
        readonly MyDbContext _dbContext = _db;

        public async Task<List<AssetDTO>> UpdateAsset(AssetDTO.AssetPersistDTO assetPersistDTO)
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
