using CompanyWork.Data;
using CompanyWork.Interfaces;
using CompanyWork.Models;
using CompanyWork.PersistClasses;
using Microsoft.EntityFrameworkCore;


namespace CompanyWork.Services.AssetServices
{

    public class AssetPost(MyDbContext db) : IPost<AssetDTO, AssetPersistDTO>
    {
        private readonly MyDbContext _db = db;

        public async Task<List<AssetDTO>> PostAsync(AssetPersistDTO assetPersistDTO) //IPost
        {
            Asset asset = new()  //add data to model
            {
                Id = Guid.NewGuid(),
                Name = assetPersistDTO.Name,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                AssetTypeId = assetPersistDTO.AssetTypeId,
            };

            await _db.Asset.AddAsync(asset); //add to db
            await _db.SaveChangesAsync();  //save to db

            List<Asset> assetList = await _db.Asset.ToListAsync();
            List<AssetDTO> assetDTO = await AssetDTO.MapAssets(_db, assetList);


            return assetDTO == null ? throw new InvalidOperationException("No AssetDTO found.") : assetDTO;
        }
    }
}
