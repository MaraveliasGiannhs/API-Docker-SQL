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

            List<AssetDTO> assetListDTO = new List<AssetDTO>();

            List<AssetType> assetTypes = await _db.AssetType.ToListAsync();

            foreach (var asset in assets)
            {
                AssetDTO assetDTO = new AssetDTO();
                assetDTO.Id = asset.Id;
                assetDTO.Name = asset.Name;
                assetDTO.CreatedAt = asset.CreatedAt;
                assetDTO.UpdatedAt = asset.UpdatedAt;

                var filteredAssetTypes = assetTypes.Where(x => x.Id == asset.AssetTypeId);

                List<AssetTypeDTO> assetTypeDTOs = await AssetTypeDTO.MapAssetTypes(_db, filteredAssetTypes.ToList()); //why list?
                assetDTO.AssetType = assetTypeDTOs?.FirstOrDefault();

                assetListDTO.Add(assetDTO);
            }

            return assetListDTO;


        }

        public class AssetPersistDTO
        {
            public Guid? Id { get; set; }
            public string Name { get; set; }
            public Guid AssetTypeId { get; set; }
        }















    }
}
