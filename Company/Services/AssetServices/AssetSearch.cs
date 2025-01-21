using CompanyWork.Data;
using System.Linq;
using CompanyWork.Interfaces;
using CompanyWork.Models;
using CompanyWork.Lookup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompanyWork.Services.AssetServices
{
    public class AssetSearch(MyDbContext db) //: ISearch<AssetDTO, Asset>, IPageData<Asset>
    {
        private readonly MyDbContext _db = db;

        public async Task<List<AssetDTO>> SearchTermAsync(AssetLookup lookup)
        {
            if (lookup == null)
                throw new NotImplementedException("Lookup term is null");


            IQueryable<Asset> assetDb = _db.Asset; // all items


            // Filters
            if (!string.IsNullOrWhiteSpace(lookup.Like))
                assetDb = assetDb.Where(a => a.Name.Contains(lookup.Like.ToLower()));

            if (lookup.Id.HasValue)
                assetDb = assetDb.Where(a => a.Id == lookup.Id.Value);

            List<Asset> assetList = await PageData(assetDb, 1, 3);
            //List<Asset> assetList = await assetDb.ToListAsync();
            List<AssetDTO> assetDTO = await AssetDTO.MapAssets(_db, assetList);

            return assetDTO;
        }




        public async Task<List<Asset>> PageData(IQueryable<Asset> data, int? selectedPage, int? pageSize)
        {
            List<Asset> listToPage = new();

            //if (!pageNumber.HasValue)
            //    return null;

            if (!pageSize.HasValue)
                return null;


            //listToPage = await data
            //    .OrderBy(d => d.Name) //check this again
            //    .Skip(pageNumber.Value * pageSize.Value) //0, 5, 10, 15 ... 
            //    .Take(pageSize.Value)
            //    .ToListAsync();



            return listToPage;

        }
    }
}
