using CompanyWork.Data;
using CompanyWork.Interfaces;
using CompanyWork.Lookup;
using CompanyWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompanyWork.Services.AssetTypeServices
{
    public class AssetTypeSearch(MyDbContext db) : ISearch<AssetTypeDTO, AssetTypeLookup>
    {
        private readonly MyDbContext _db = db;

        public async Task<List<AssetTypeDTO>> SearchTermAsync(AssetTypeLookup lookup)
        {
            if (lookup == null)
                throw new NotImplementedException("Lookup term is null");


            IQueryable<AssetType> assetTypeDb = _db.AssetType;


            // Filters
            if (!string.IsNullOrWhiteSpace(lookup.Like))
                assetTypeDb = assetTypeDb.Where(a => a.Name.Contains(lookup.Like.ToLower()));

            if (lookup.Id.HasValue)
                assetTypeDb = assetTypeDb.Where(a => a.Id == lookup.Id.Value);


            List<AssetType> assetTypeList = await assetTypeDb.ToListAsync();
            List<AssetTypeDTO> assetTypeDTOList = await AssetTypeDTO.MapAssetTypes(_db, assetTypeList);


            //assetTypeDb = await AssetTypeDTO.MapAssetTypes(_db, assetTypeDb);
            //var searchTerm = await assetTypeDb.ToListAsync(); 

            return assetTypeDTOList;
        }
    }
}
