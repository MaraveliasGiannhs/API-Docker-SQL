using CompanyWork.Models;
using CompanyWork.PersistClasses;
using static CompanyWork.Models.AssetDTO;

namespace CompanyWork.Interfaces
{
    public interface IPost<T,C>
    {
        Task<List<T>> PostAsync(C persistDTO);

    }

}
