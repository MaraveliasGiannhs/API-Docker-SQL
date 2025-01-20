namespace CompanyWork.Interfaces
{
    public interface IDelete
    {
        Task<IResult> DeleteAsset(Guid id);
    }
}
