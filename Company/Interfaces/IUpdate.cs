using CompanyWork.Models;
using CompanyWork.PersistClasses;
using static CompanyWork.Models.AssetDTO;

namespace CompanyWork.Interfaces
{
    public interface IUpdate<T, C>
    {
        Task<List<T>> UpdateAsync(C persistDTO);

    }
}
