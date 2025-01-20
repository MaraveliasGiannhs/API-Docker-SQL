using CompanyWork.Models;
using Microsoft.AspNetCore.Mvc;

namespace CompanyWork.Interfaces
{
    public interface IGetById<T>
    {
        Task<ActionResult<List<T>>> GetByIdAsync(Guid id);
    }
}
