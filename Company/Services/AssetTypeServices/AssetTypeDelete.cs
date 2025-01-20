using CompanyWork.Data;
using CompanyWork.Interfaces;

namespace CompanyWork.Services.AssetTypeServices
{
    public class AssetTypeDelete(MyDbContext db) : IDelete
    {
        private readonly MyDbContext _db = db;
        public async void DeleteAsync(Guid id)
        {
            var assetType = await _db.AssetType.FindAsync(id);

            if (assetType == null)
                throw new NotImplementedException();

            _db.AssetType.Remove(assetType);
            await _db.SaveChangesAsync();

        }
    }
}
