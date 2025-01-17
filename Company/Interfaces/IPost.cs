using CompanyWork.Models;
using static CompanyWork.Models.AssetDTO;

namespace CompanyWork.Interfaces
{
    public interface IPost
    {
        Task<List<AssetDTO>> GetAssetsAsync(AssetPersistDTO assetPersistDTO);

    }
}
