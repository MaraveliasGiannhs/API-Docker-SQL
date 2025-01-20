using CompanyWork.Data;
using CompanyWork.Interfaces;
using CompanyWork.Models;
using Microsoft.AspNetCore.Mvc;

namespace CompanyWork.Services
{
    public class AssetGetById(MyDbContext db) : IGetById
    {
        private readonly MyDbContext _db = db;

        public async Task<ActionResult<List<AssetDTO>>> ReadAsset(Guid id)
        {
            var asset = await _db.Asset.FindAsync(id);

            if (asset == null)
                throw new NotImplementedException("Asset is null");

            List<AssetDTO> assetDTO = await AssetDTO.MapAssets(_db, new List<Asset> { asset });

            return assetDTO;
        }
    }
}
