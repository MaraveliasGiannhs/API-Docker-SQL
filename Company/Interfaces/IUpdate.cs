using CompanyWork.Models;
using static CompanyWork.Models.AssetDTO;

namespace CompanyWork.Interfaces
{
    public interface IUpdate
    {
        Task<List<AssetDTO>> UpdateAsset(AssetPersistDTO assetPersistDTO);

    }
}
