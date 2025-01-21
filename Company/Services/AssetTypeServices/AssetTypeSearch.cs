using CompanyWork.Data;
using CompanyWork.Interfaces;
using CompanyWork.Lookup;
using CompanyWork.Models;
using Microsoft.EntityFrameworkCore;
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

        public AssetTypeSearch Ids(Guid id)
        {
            _id = id;
            return this;
        }

        public AssetTypeSearch Names(string name)
        {
            _name = name;
            return this;
        }
 
        public async Task<List<AssetTypeDTO>> SearchAsync()
        {

            //if (!_id.HasValue)
            //    return null;
            //if (_name == null)
            //    return null;


            IQueryable<AssetType> assetTypeDb = _db.AssetType;


            // If not empty, add Filters
            if (!string.IsNullOrWhiteSpace(_name))
                assetTypeDb = assetTypeDb.Where(a => a.Name.Contains(_name.ToLower()));

            if (this._id != null)
                assetTypeDb = assetTypeDb.Where(a => a.Id == this._id);


            List<AssetType> assetTypeList = await PageData(assetTypeDb, 1, 3);
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


        public async Task<List<AssetType>> PageData(IQueryable<AssetType> data, int? pageNumber, int? pageSize)
        {
            int allItems = 0;


            if (!pageSize.HasValue)
                pageSize = 1;

            if (!pageNumber.HasValue)
                pageNumber = 1;

            List<AssetType> listToPage = new();
            listToPage = await data
                .OrderBy(d => d.Name)
                .Skip(pageNumber.Value * pageSize.Value) //0, 5, 10, 15 ... 
                .Take(pageSize.Value)
                .ToListAsync();

            foreach (var item in listToPage) //slightly faster 
                allItems++;
            Console.WriteLine("All items:" + allItems);

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
