 using CompanyWork.Data;

namespace CompanyWork.Models
{
    public class AssetTypeDTO
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }



        public static async Task<List<AssetTypeDTO>> MapAssetTypes(MyDbContext _db, List<AssetType> assetTypes)
        {
            List<AssetTypeDTO> assetTypeDTOList = new List<AssetTypeDTO>();

            foreach (var a in assetTypes)
            {
                AssetTypeDTO assetType = new AssetTypeDTO();
                assetType.Id = a.Id;
                assetType.Name = a.Name;

                assetTypeDTOList.Add(assetType);
            }

            return assetTypeDTOList;
        }

    }
}
