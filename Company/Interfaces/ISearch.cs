using CompanyWork.Lookup;
using CompanyWork.Models;
using Microsoft.AspNetCore.Mvc;

namespace CompanyWork.Interfaces
{
    public interface ISearch
    {
        Task<ActionResult<List<AssetDTO>>> SearchTerm(AssetLookup lookup);

    }
}
