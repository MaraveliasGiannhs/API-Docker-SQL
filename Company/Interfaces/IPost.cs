using CompanyWork.Models;
using CompanyWork.PersistClasses;
using static CompanyWork.Models.AssetDTO;

namespace CompanyWork.Interfaces
{
    public interface IPostUpdate<T,C>
    {
        Task<List<T>> PostUpdateAsync(C persistDTO);

    }

}
