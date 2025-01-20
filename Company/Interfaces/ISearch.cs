using CompanyWork.Lookup;
using CompanyWork.Models;
using Microsoft.AspNetCore.Mvc;

namespace CompanyWork.Interfaces
{
    public interface ISearch<T,C>
    {
        Task<List<T>> SearchTermAsync(C lookup);
    }
}
