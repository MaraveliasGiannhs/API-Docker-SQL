using Company.Data;

namespace Company.Models
{
    public class AssetTypeDTO
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }



        public static async Task<List<AssetTypeDTO>> MapAssetTypes(MyDbContext _db, List<AssetTypeModel> assetTypes)
        {
            List<AssetTypeDTO> assetTypeDTO = new List<AssetTypeDTO>();

            foreach (var a in assetTypes)
            {
                AssetTypeDTO assetType = new AssetTypeDTO();
                assetType.Id = a.Id;
                assetType.Name = a.Name;
                assetTypeDTO.Add(assetType);
            }

            return assetTypeDTO;
        }

    }
}
