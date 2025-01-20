using Microsoft.EntityFrameworkCore;
using CompanyWork.Data;
using CompanyWork.Models;

namespace CompanyWork.Models
{
    public class AssetDTO
    {

        public Guid? Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        //public Guid AssetTypeId { get; set; }
        public AssetTypeDTO AssetType { get; set; } //id, name

        public static async Task<List<AssetDTO>> MapAssets(MyDbContext _db, List<Asset> assets)
        {

            List<AssetDTO> assetListDTO = new();

            List<AssetType> assetTypesDb = await _db.AssetType.ToListAsync();

            foreach (var asset in assets)
            {
                AssetDTO assetDTO = new()
                {
                    Id = asset.Id,
                    Name = asset.Name,
                    CreatedAt = asset.CreatedAt,
                    UpdatedAt = asset.UpdatedAt,
                };

                var filteredAssetTypes = assetTypesDb.Where(x => x.Id == asset.AssetTypeId);

                List<AssetTypeDTO> assetTypeDTOs = await AssetTypeDTO.MapAssetTypes(_db, filteredAssetTypes.ToList()); 
                assetDTO.AssetType = assetTypeDTOs?.FirstOrDefault();

                assetListDTO.Add(assetDTO);
            }

            return assetListDTO;


        }

       















    }
}
