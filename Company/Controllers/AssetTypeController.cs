using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CompanyWork.Data;
using CompanyWork.Models;
using CompanyWork.Lookup;
using System.Collections.Generic;
using CompanyWork.PersistClasses;
using CompanyWork.Services.AssetTypeServices;




namespace CompanyWork.Controllers
{
    [ApiController]
    [Route("api/assetTypes")] 
    public class AssetTypeController
    {

        private readonly ILogger<AssetTypeController> _logger;
        private readonly MyDbContext _db;

        private readonly AssetTypePostUpdate _assetTypePost;
        private readonly AssetTypeDelete _assetTypeDelete;  
        private readonly AssetTypeSearch _assetTypeSearch;
        private readonly AssetTypeGetById _assetTypeGetById;

        public AssetTypeController(
            ILogger<AssetTypeController> logger,
            MyDbContext db,
            AssetTypePostUpdate assetTypePost,
            AssetTypeDelete assetTypeDelete,
            AssetTypeSearch assetTypeSearch,
            AssetTypeGetById assetTypeGetById)
        {
            _logger = logger;
            _db = db;
            _assetTypePost = assetTypePost;
            _assetTypeDelete = assetTypeDelete;
            _assetTypeSearch = assetTypeSearch;
            _assetTypeGetById = assetTypeGetById;
        }



        [HttpPost]
        public async Task<List<AssetTypeDTO>> CreateAssetType(AssetTypePersistDTO assetTypePersist)
        {
            return await _assetTypePost.PostUpdateAsync(assetTypePersist);
        }





        [HttpGet("{id}")]
        public async Task<ActionResult<List<AssetTypeDTO>>> ReadAssetType(Guid id)
        {
            return await _assetTypeGetById.GetByIdAsync(id);
        }




        [HttpPost("search")] //+ReadAll
        public async Task<List<AssetTypeDTO>> SearchTerm(AssetTypeLookup lookup)
        {
            return await _assetTypeSearch.SearchTermAsync(lookup);
        }




        [HttpDelete("{id}")]
        public void DeleteAssetType(Guid id)
        {
            _assetTypeDelete.DeleteAsync(id);

        }
    }
}
