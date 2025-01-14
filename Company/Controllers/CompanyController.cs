using Company.Data;
using Company.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.Controllers
{
    [ApiController]
    [Route("api/companies")]
    public class CompanyController : ControllerBase
    {
        private readonly ILogger<CompanyController> _logger;
        private readonly MyDbContext _db;

        public CompanyController(ILogger<CompanyController> logger, MyDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpPost]
        public async Task<IResult> Create(CompanyModelDTO companyDTO)
        {

            var company = new CompanyModel()
            {
                Id = Guid.NewGuid(),
                Name = companyDTO.Name,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,

            };

            await _db.Company.AddAsync(company);
            
            await _db.SaveChangesAsync();

            CompanyModelDTO newCompanyDTO = new()
            {
                Id = company.Id,
                Name = company.Name,
                CreatedAt = company.CreatedAt,
                UpdatedAt = company.UpdatedAt,
            };

            return TypedResults.Ok(newCompanyDTO);
        }





        [HttpGet("{id}")]
        public async Task<IResult> Read(Guid id)
        {
            CompanyModel? company;            
            company = await _db.Company.FindAsync(id);
            
            if(company == null)
                return TypedResults.NotFound(company);

            var companyDTO = new CompanyModelDTO()
            {
                Id = company.Id,
                Name = company.Name,
                CreatedAt = company.CreatedAt,
                UpdatedAt = company.UpdatedAt,
            }; 
            return TypedResults.Ok(companyDTO); 
        }




        [HttpGet]
        public async Task<IResult> ReadAll(MyDbContext _db)
        {
            var companyDTO = await _db.Company.Select(company => new CompanyModelDTO()
            {
                Id = company.Id,
                Name = company.Name,
                CreatedAt = company.CreatedAt,
                UpdatedAt = company.UpdatedAt,

            }).ToListAsync();

            return TypedResults.Ok(companyDTO);
            
        }





        [HttpPut("{id}")]
        public async Task<IResult> Update(Guid id, CompanyModelDTO companyDTO )
        {
            var company = await _db.Company.FindAsync(id);

            if (company == null)
                return TypedResults.NotFound(company);


            company.Name = companyDTO.Name;
            company.UpdatedAt = DateTime.UtcNow;
            //company.CreatedAt = companyDTO.CreatedAt;

            CompanyModelDTO newCompanyDTO = new()
            {
                Id = company.Id,
                Name = company.Name,
                CreatedAt = company.CreatedAt,
                UpdatedAt = company.UpdatedAt

            };
            
            await _db.SaveChangesAsync();

            return TypedResults.Ok(newCompanyDTO);
            
        }




        [HttpDelete("{id}")]
        public async Task<IResult> Delete(Guid id)
        {
            var company = await _db.Company.FindAsync(id);

            if (company == null)
                return TypedResults.NotFound();

            _db.Company.Remove(company);
            await _db.SaveChangesAsync();

            return TypedResults.NoContent();

        }
    }
}
