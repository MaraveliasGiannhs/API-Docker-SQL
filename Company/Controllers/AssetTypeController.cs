using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CompanyWork.Data;
using CompanyWork.Models;
using CompanyWork.Lookup;
using System.Collections.Generic;
using CompanyWork.PersistClasses;
using CompanyWork.Services.AssetTypeServices;
using NPOI.OpenXmlFormats.Spreadsheet;




namespace CompanyWork.Controllers
{
    [ApiController]
    [Route("api/assetTypes")] 
    public class AssetTypeController
    {

        private readonly ILogger<AssetTypeController> _logger;
        private readonly MyDbContext _db;

        private readonly AssetTypeSearch _assetTypeSearch;
        private readonly AssetTypePostUpdate _assetTypePost;
        private readonly AssetTypeDelete _assetTypeDelete;  
        private readonly IServiceProvider serviceProvider;





        public AssetTypeController(
            ILogger<AssetTypeController> logger,
            MyDbContext db,
            AssetTypePostUpdate assetTypePost,
            AssetTypeDelete assetTypeDelete,
            AssetTypeSearch assetTypeSearch
            )
        {
            _logger = logger;
            _db = db;
            _assetTypePost = assetTypePost;
            _assetTypeDelete = assetTypeDelete;
            _assetTypeSearch = assetTypeSearch;
        }




        [HttpPost]
        public async Task<List<AssetTypeDTO>> CreateAssetType(AssetTypePersistDTO assetTypePersist)
        {
            return await _assetTypePost.PostUpdateAsync(assetTypePersist);
        }




        [HttpPost("search")] //+ReadAll
        public async Task<List<AssetTypeDTO>> SearchTerm(AssetTypeLookup lookup)
        {
            //Test aa = this.serviceProvider.GetRequiredService<Test>();
            //aa.Log();
            //aa.aa = "aaa";
            //aa.Log();
            //aa = this.serviceProvider.GetRequiredService<Test>();
            //aa.Log();

            //AssetTypeSearch assetTypeSearch = serviceProvider.GetRequiredService<AssetTypeSearch>();



            //fluent pattern
            // + checkNull method
            if (lookup.Id.HasValue)
                _assetTypeSearch.Ids(lookup.Id.Value); 

            if (!string.IsNullOrEmpty(lookup.Like))
                _assetTypeSearch.Names(lookup.Like);

            if (lookup.PageIndex.HasValue)
                _assetTypeSearch.PageIndex(lookup.PageIndex.Value);

            if (lookup.ItemsPerPage.HasValue)
                _assetTypeSearch.PageSize(lookup.ItemsPerPage.Value);

            if (!string.IsNullOrEmpty(lookup.OrderItem))
                _assetTypeSearch.OrderBy(lookup.OrderItem);

            if (lookup.AscendingOrder.HasValue)
                _assetTypeSearch.Ascending(lookup.AscendingOrder.Value);
            
            
            return await _assetTypeSearch.SearchAsync();
        }




        [HttpGet]
        public async Task<int> GetElementSum()
        {
            return await _assetTypeSearch.Count();
        }



        [HttpDelete("{id}")]
        public async Task DeleteAssetType(Guid id)
        {
            await _assetTypeDelete.DeleteAsync(id);
        }
    }
}
