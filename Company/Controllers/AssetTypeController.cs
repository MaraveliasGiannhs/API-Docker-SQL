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

        private readonly AssetTypePost _assetTypePost;
        private readonly AssetTypeUpdate _assetTypeUpdate;
        private readonly AssetTypeDelete _assetTypeDelete;  
        private readonly AssetTypeSearch _assetTypeSearch;
        private readonly AssetTypeGetById _assetTypeGetById;

        public AssetTypeController(
            ILogger<AssetTypeController> logger,
            MyDbContext db,
            AssetTypePost assetTypePost,
            AssetTypeUpdate assetTypeUpdate,
            AssetTypeDelete assetTypeDelete,
            AssetTypeSearch assetTypeSearch,
            AssetTypeGetById assetTypeGetById)
        {
            _logger = logger;
            _db = db;
            _assetTypePost = assetTypePost;
            _assetTypeUpdate = assetTypeUpdate;
            _assetTypeDelete = assetTypeDelete;
            _assetTypeSearch = assetTypeSearch;
            _assetTypeGetById = assetTypeGetById;
        }



        [HttpPost]
        public async Task<List<AssetTypeDTO>> CreateAssetType(AssetTypePersistDTO assetTypePersist)
        {
            if (!assetTypePersist.Id.HasValue) //create
            {
                return await _assetTypePost.PostAsync(assetTypePersist);
            }
            else //update
            {
                return await _assetTypeUpdate.UpdateAsync(assetTypePersist);    
            }
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
        public async Task<IResult> DeleteAssetType(Guid id)
        {
            return await _assetTypeDelete.DeleteAsync(id);

        }
    }
}
