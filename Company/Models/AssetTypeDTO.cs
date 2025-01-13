using System.Data.Entity;
using Company.Data;

namespace Company.Models
{
    public class AssetTypeDTO
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }



        public async Task<List<AssetTypeDTO>> MapFields(MyDbContext _db)
        {
            var assets = await _db.Asset.Select(a => a.AssetTypeId).ToListAsync(); //select all AssetTypeIds

            var assetTypes = await _db.AssetType.Where(a => assets.Contains(a.Id)).Select(assetType => new AssetTypeDTO() //filter, select and map
            {
                Id = assetType.Id,
                Name = assetType.Name,
            }).ToListAsync();

            return assetTypes;
        }

    }
}
