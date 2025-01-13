using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
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

        public async Task<List<AssetModelDTO>> MapFields(MyDbContext _db, List<AssetModel> asset, List<AssetTypeDTO> assetTypes)
        {
        
            //foreach (var assetType in assetTypes)
            //{
            //}

            var assetDTO = asset.Select(asset => new AssetModelDTO()
            {
                Id = asset.Id,
                Name = asset.Name,
                CreatedAt = asset.CreatedAt,
                UpdatedAt = asset.UpdatedAt,
                AssetType = assetTypes.FirstOrDefault(x => x.Id == asset.AssetTypeId)
            }).ToList();

            return assetDTO;

        }
    }















}
