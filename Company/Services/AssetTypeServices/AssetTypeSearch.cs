using CompanyWork.Data;
using CompanyWork.Interfaces;
using CompanyWork.Lookup;
using CompanyWork.Models;
using Microsoft.EntityFrameworkCore;
using NPOI.OpenXmlFormats.Wordprocessing;
using static System.Runtime.InteropServices.JavaScript.JSType;


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
 
        public async Task<List<AssetTypeDTO>> SearchAsync()
        {
            Console.WriteLine("Index:" + _pageIndex);
            Console.WriteLine("Items per page:" + _itemsPerPage);

          
            //if (!_id.HasValue)
            //    return null;
            //if (_name == null)
            //    return null;


            IQueryable<AssetType> assetTypeDb = _db.AssetType;


            // If not empty, add Filters
            if (!string.IsNullOrWhiteSpace(_name))
                assetTypeDb = assetTypeDb.Where(a => a.Name.Contains(_name.ToLower()));

            if (this._id.HasValue)//get by id
                assetTypeDb = assetTypeDb.Where(a => a.Id == this._id);


            List<AssetType> assetTypeList = await PageData(assetTypeDb, _pageIndex, _itemsPerPage);
            List<AssetTypeDTO> assetTypeDTOList = await AssetTypeDTO.MapAssetTypes(_db, assetTypeList);


            //assetTypeDb = await AssetTypeDTO.MapAssetTypes(_db, assetTypeDb);
            //var searchTerm = await assetTypeDb.ToListAsync(); 

            return assetTypeDTOList;
        }


        public async Task<int> Count()
        {
            IQueryable<AssetType> assetTypeDb = _db.AssetType;

            //apply filters 
            

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

            //foreach (var item in listToPage) //slightly faster than Count()
            //    allItems++;
            //Console.WriteLine("All items:" + allItems);

            //front
            //float pageNumber = allItems / pageSize.Value; //find total pages needed 
            //Console.WriteLine("Pages needed (not rounded up):" + pageNumber);

            //front
            //if (pageNumber % 1 != 0) //round up a page to fit all items
            //    pageNumber = (int)Math.Ceiling(pageNumber); // <-return total pages needed to front display 
            //Console.WriteLine("Pages needed (rounded sum):" + pageNumber);






            return listToPage;

        }

    }
}
