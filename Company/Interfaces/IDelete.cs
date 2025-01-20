namespace CompanyWork.Interfaces
{
    public interface IDelete
    {
        Task<IResult> DeleteAsync(Guid id);
    }
}
