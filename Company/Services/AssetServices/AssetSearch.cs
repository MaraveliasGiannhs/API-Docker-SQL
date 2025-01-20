using CompanyWork.Data;
using System.Linq;
using CompanyWork.Interfaces;
using CompanyWork.Models;
using CompanyWork.Lookup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompanyWork.Services.AssetServices
{
    public class AssetSearch(MyDbContext db) : ISearch<AssetDTO, AssetLookup>
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


            List<Asset> assetList = await assetDb.ToListAsync();
            List<AssetDTO> assetDTO = await AssetDTO.MapAssets(_db, assetList);

            return assetDTO;
        }
    }
}
