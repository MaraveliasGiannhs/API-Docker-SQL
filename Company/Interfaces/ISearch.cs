using CompanyWork.Lookup;
using CompanyWork.Models;
using Microsoft.AspNetCore.Mvc;

namespace CompanyWork.Interfaces
{
    public interface ISearch<T,U>
    {
        Task<List<T>> SearchAsync();
        Task<List<U>> PageData(IQueryable<U> data, int? selectedPage, int? pageSize);

        //Task<T> First();
        //Task<int> Count();
    }
}
