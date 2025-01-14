using System.Data.Entity;
using Company.Data;

namespace Company.Models
{
    public class AssetTypeDTO
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }



        public static async Task<List<AssetTypeDTO>> MapFields(MyDbContext _db, List<AssetTypeModel> assetTypes)
        {
            List<AssetTypeDTO> assetTypeDTO = new List<AssetTypeDTO>();
            var asset = _db.Asset.FindAsync();

            foreach (var a in assetTypes)
            {

                AssetTypeDTO assetType = new AssetTypeDTO();
                assetType.Id = a.Id;
                assetType.Name = a.Name;
                if(assetType.Id == asset.AssetTypeId)
                assetTypeDTO.Add(assetType);

            }

            return assetTypeDTO;


            //var assets = asset.Select(a => a.AssetTypeId).ToList(); //select all AssetTypeIds

            //var assetTypes = assetType.Where(a => assets.Contains(a.Id)).Select(assetType => new AssetTypeDTO() //filter, select and map
            //{
            //    Id = assetType.Id,
            //    Name = assetType.Name,
            //}).ToList();

        }

    }
}
