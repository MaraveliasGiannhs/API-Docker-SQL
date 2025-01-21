using CompanyWork.Data;
using CompanyWork.Interfaces;
using System.Data.Entity;
using Microsoft.EntityFrameworkCore;



namespace CompanyWork.Utils
{
    public class PagingService<T>(MyDbContext db)
    {
        private readonly MyDbContext _db = db;
        public async Task<List<T>> PageData(IQueryable<T> data, int? pageNumber, int? pageSize)
        {
            List<T> listToPage = new();

            if (!pageNumber.HasValue)
                return null;

            if (!pageSize.HasValue)
                return null;


            listToPage = await data
                .OrderBy(d => data) //check this again
                .Skip(pageNumber.Value * pageSize.Value) //0, 5, 10, 15 ... 
                .Take(pageSize.Value)
                .ToListAsync();



            return listToPage;

        }
    }
}
