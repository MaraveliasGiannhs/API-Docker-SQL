using CompanyWork.Data;
using CompanyWork.Interfaces;

namespace CompanyWork.Services
{

    public class AssetDelete(MyDbContext db) : IDelete
    {
        private readonly MyDbContext _db = db;

        public async Task<IResult> DeleteAsset(Guid id)
        {
            var asset = await _db.Asset.FindAsync(id);

            if (asset == null)
                return TypedResults.NotFound();

            _db.Asset.Remove(asset);
            await _db.SaveChangesAsync();

            return TypedResults.NoContent();

        }
    }
}
