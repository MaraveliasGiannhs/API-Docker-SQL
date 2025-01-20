using CompanyWork.Data;
using CompanyWork.Interfaces;
using CompanyWork.Models;
using CompanyWork.PersistClasses;
using Microsoft.EntityFrameworkCore;


namespace CompanyWork.Services.AssetServices
{

    public class AssetPostUpdate(MyDbContext db) : IPostUpdate<AssetDTO, AssetPersistDTO>
    {
        private readonly MyDbContext _db = db;

        public async Task<List<AssetDTO>> PostUpdateAsync(AssetPersistDTO assetPersistDTO) //IPost
        {
            if (!assetPersistDTO.Id.HasValue) //post
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
            else
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
}
