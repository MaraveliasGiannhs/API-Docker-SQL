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

            foreach (var assetTypeItem in assetTypes)
            {
                AssetTypeDTO assetTypeDTO = new();

                assetTypeDTO.Id = assetTypeItem.Id;
                assetTypeDTO.Name = assetTypeItem.Name;

                assetTypeDTOList.Add(assetTypeDTO);
            }

            return assetTypeDTOList;
        }

    }
}
