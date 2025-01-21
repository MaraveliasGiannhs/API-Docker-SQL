using CompanyWork.Data;
using CompanyWork.Interfaces;

namespace CompanyWork.Services.AssetServices
{

    public class AssetDelete(MyDbContext db) : IDelete
    {
        private readonly MyDbContext _db = db;
        public async Task DeleteAsync(Guid id)
        {
            var asset = await _db.Asset.FindAsync(id);

            if (asset == null) {
                throw new ApplicationException();
            }

            _db.Asset.Remove(asset);
            await _db.SaveChangesAsync();

            
        }
    }
}
