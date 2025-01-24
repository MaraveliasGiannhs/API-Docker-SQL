using CompanyWork.Data;
using CompanyWork.Models;
using CompanyWork.Lookup;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CompanyWork.Services.AssetServices
{
    public class AssetSearch(MyDbContext db) //: ISearch<AssetDTO, Asset>, IPageData<Asset>
    {
        private readonly MyDbContext _db = db;

        private Guid? _id;
        private string? _name;
        private int? _pageIndex;
        private int? _itemsPerPage;
        private string? _orderByItem;
        private bool _ascending;

        public AssetSearch Ids(Guid id)
        {
            _id = id;
            return this;//for chaining
        }

        public AssetSearch Names(string name)
        {
            _name = name;
            return this;
        }

        public AssetSearch PageIndex(int pageIndex)
        {
            _pageIndex = pageIndex;
            return this;
        }

        public AssetSearch PageSize(int itemsPerPage)
        {
            _itemsPerPage = itemsPerPage;
            return this;
        }
        public AssetSearch OrderBy(string? orderByItem)
        {
            _orderByItem = orderByItem;
            return this;
        }

        public AssetSearch Ascending(bool ascending)
        {
            _ascending = ascending;
            return this;
        }

        public async Task<List<AssetDTO>> SearchTermAsync(AssetLookup lookup)
        {
            if (lookup == null)
                throw new NotImplementedException("Lookup term is null");

            IQueryable<Asset> assetDb = _db.Asset; // all items

            // Filters
            if (!string.IsNullOrWhiteSpace(this._name))
                assetDb = assetDb.Where(a => a.Name.Contains(this._name.ToLower()));

            if (this._id.HasValue)
                assetDb = assetDb.Where(a => a.Id == this._id);

            if (!string.IsNullOrEmpty(this._orderByItem))
                assetDb = OrderByDynamically(assetDb, this._orderByItem, this._ascending);

            Console.WriteLine(_orderByItem);
            Console.WriteLine(_ascending);


            List<Asset> assetList = await PageData(assetDb, _pageIndex, _itemsPerPage);
            List<AssetDTO> assetDTO = await AssetDTO.MapAssets(_db, assetList);

            return assetDTO;
        }


        public async Task<int> Count()
        {
            IQueryable<Asset> assetDb = _db.Asset;

            //foreach (var item in listToPage) //slightly faster than linq Count
            //    allItems++;
            //Console.WriteLine("All items:" + allItems);
            return await assetDb.CountAsync();
        }


        public IQueryable<Asset> OrderByDynamically(IQueryable<Asset> data, string orderByItem, bool ascending)
        {
            var parameter = Expression.Parameter(typeof(Asset), "a"); 
            var property = Expression.Property(parameter, orderByItem); 

            var lambda = Expression.Lambda<Func<Asset, object>>(
                Expression.Convert(property, typeof(object)),//body 
                parameter); //input (a)


            if (!ascending)
                return data.OrderByDescending(lambda);
            else
                return data.OrderBy(lambda);
        }

        public async Task<List<Asset>> PageData(IQueryable<Asset> data, int? selectedPage, int? pageSize)
        {
            List<Asset> listToPage = new();

            if (!selectedPage.HasValue)
                throw new ApplicationException();

            if (!pageSize.HasValue)
                throw new ApplicationException();


            listToPage = await data
                .Skip((selectedPage.Value - 1) * pageSize.Value) //0, 5, 10, 15 ... 
                .Take(pageSize.Value)
                .ToListAsync();

            return listToPage;
        }
    }
}
