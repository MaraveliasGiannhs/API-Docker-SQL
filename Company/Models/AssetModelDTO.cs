using Microsoft.EntityFrameworkCore;
using Company.Data;

namespace Company.Models
{
    public class AssetModelDTO
    {

        public Guid? Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        //public Guid AssetTypeId { get; set; }
        public AssetTypeDTO AssetType { get; set; } //id, name

        public static async Task<List<AssetModelDTO>> MapAssets(MyDbContext _db, List<AssetModel> asset)
        {

            List<AssetModelDTO> assetDTO = new List<AssetModelDTO>();

            var assetTypes = await _db.AssetType.ToListAsync();

            foreach (var a in asset)
            {
                AssetModelDTO assetModelDTO = new AssetModelDTO();
                assetModelDTO.Id = a.Id;
                assetModelDTO.Name = a.Name;
                assetModelDTO.CreatedAt = a.CreatedAt;
                assetModelDTO.UpdatedAt = a.UpdatedAt;

                var filteredAssetTypes = assetTypes.Where(x => x.Id == a.AssetTypeId );

                List<AssetTypeDTO> assetTypeDTOs = await AssetTypeDTO.MapAssetTypes(_db, filteredAssetTypes.ToList()); //why list?
                assetModelDTO.AssetType = assetTypeDTOs?.FirstOrDefault();

                assetDTO.Add(assetModelDTO);
            }

            return assetDTO;


            //var assetDTO = asset.Select(asset => new AssetModelDTO()
            //{
            //    Id = asset.Id,
            //    Name = asset.Name,
            //    CreatedAt = asset.CreatedAt,
            //    UpdatedAt = asset.UpdatedAt,
            //    AssetType = assetTypes.FirstOrDefault(x => x.Id == asset.AssetTypeId)
            //}).ToList();

        }
    }















}
