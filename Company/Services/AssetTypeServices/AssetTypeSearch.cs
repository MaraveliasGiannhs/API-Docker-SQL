using System.Linq.Expressions;
using CompanyWork.Data;
using CompanyWork.Interfaces;
using CompanyWork.Models;
using Microsoft.EntityFrameworkCore;



namespace CompanyWork.Services.AssetTypeServices
{
    //public class Test
    //{
    //    public string aa { get; set; }
    //    public void Log() => Console.WriteLine(aa);
    //}

    public class AssetTypeSearch(MyDbContext db) : ISearch<AssetTypeDTO, AssetType>
    {
        private readonly MyDbContext _db = db;

        private Guid? _id;
        private string? _name;
        private int? _pageIndex;
        private int? _itemsPerPage;
        private string? _orderByItem;
        private bool _ascending;
        public AssetTypeSearch Ids(Guid id) 
        {
            _id = id;
            return this;//for chaining
        }

        public AssetTypeSearch Names(string name) 
        {
            _name = name;
            return this;
        }

        public AssetTypeSearch PageIndex(int pageIndex)
        {
            _pageIndex = pageIndex;
            return this;
        }

        public AssetTypeSearch PageSize(int itemsPerPage)
        {
            _itemsPerPage = itemsPerPage;
            return this;
        }
        public AssetTypeSearch OrderBy(string? orderByItem)
        {
            _orderByItem = orderByItem;
            return this;
        }

        public AssetTypeSearch Ascending(bool ascending)
        {
            _ascending = ascending;
            return this;
        }

        public async Task<List<AssetTypeDTO>> SearchAsync()
        {
            IQueryable<AssetType> assetTypeDb = _db.AssetType;


            // Filters
            if (!string.IsNullOrWhiteSpace(_name))
                assetTypeDb = assetTypeDb.Where(a => a.Name.Contains(_name.ToLower()));

            if (this._id.HasValue)//get by id
                assetTypeDb = assetTypeDb.Where(a => a.Id == this._id);

            if (!string.IsNullOrEmpty(_orderByItem))
            {
                Console.WriteLine("not NULL ORDER ITEM");
                Console.WriteLine(_orderByItem); //ok
                Console.WriteLine(_ascending); //ok

                assetTypeDb = OrderByDynamically(assetTypeDb, _orderByItem, _ascending);
            }
            else
                Console.WriteLine("NULL ORDER ITEM");



            List<AssetType> assetTypeList = await PageData(assetTypeDb, _pageIndex, _itemsPerPage);
            List<AssetTypeDTO> assetTypeDTOList = await AssetTypeDTO.MapAssetTypes(_db, assetTypeList);


            //assetTypeDb = await AssetTypeDTO.MapAssetTypes(_db, assetTypeDb);
            //var searchTerm = await assetTypeDb.ToListAsync(); 

            return assetTypeDTOList;
        }

        //make it generic to use on other models
        public IQueryable<AssetType> OrderByDynamically(IQueryable<AssetType> data ,string orderByItem, bool ascending)
        {
            var parameter = Expression.Parameter(typeof(AssetType), "a"); //input of lamda func representing AssetType model
            var property = Expression.Property(parameter, orderByItem); //access AssetType's property with name of _orderByItem value

            //parameter => property
            //        a => a.field(a.id, a.name, etc...)

            var lambda = Expression.Lambda<Func<AssetType, object>>(
                Expression.Convert(property, typeof(object)),//body of lambda, return value (a.id) //convert expression to object for OrderBy
                parameter); //input (a)
            
                       
            
            if (!ascending)
                return data.OrderByDescending(lambda);
            else
                return data.OrderBy(lambda);
            //check data value after ordering
            //return data; wtf


   
        }




        public async Task<int> Count()
        {
            IQueryable<AssetType> assetTypeDb = _db.AssetType;

            //apply filters 


            //foreach (var item in listToPage) //slightly faster than linq Count
            //    allItems++;
            //Console.WriteLine("All items:" + allItems);

            return await assetTypeDb.CountAsync();
        }




        public async Task<List<AssetType>> PageData(IQueryable<AssetType> data, int? pageIndex, int? itemsPerPage)
        {

            if (!itemsPerPage.HasValue)
                itemsPerPage = 1;

            if (!pageIndex.HasValue)
                pageIndex = 1;

            List<AssetType> listToPage = new();
            listToPage = await data
                //.OrderBy(d => d.Name)
                .Skip((pageIndex.Value - 1) * itemsPerPage.Value) //0, 5, 10, 15 ... 
                .Take(itemsPerPage.Value) //index
                .ToListAsync();


            return listToPage;

        }

    }
}
