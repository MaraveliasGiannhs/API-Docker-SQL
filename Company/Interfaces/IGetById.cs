using CompanyWork.Models;
using Microsoft.AspNetCore.Mvc;

namespace CompanyWork.Interfaces
{
    public interface IGetById
    {
        Task<ActionResult<List<AssetDTO>>> ReadAsset(Guid id);
    }
}
