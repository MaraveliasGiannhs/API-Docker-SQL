using Company.Data;
using Company.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.Controllers
{
    [ApiController]
    [Route("api/branches")] //?
    public class BranchController : ControllerBase
    {


        private readonly ILogger<BranchController> _logger;
        private readonly MyDbContext _db;

        public BranchController(ILogger<BranchController> logger, MyDbContext db)
        {
            _logger = logger;
            _db = db;
        }




        [HttpPost] //?
        public async Task<IResult> CreateBranch(BranchModelDTO branchDTO)
        {

            var branch = new BranchModel()
            {
                Id = Guid.NewGuid(),
                Name = branchDTO.Name,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                CompanyId = branchDTO.CompanyId

            };

            await _db.Branch.AddAsync(branch);

            await _db.SaveChangesAsync();

            var newBranchDTO = new BranchModelDTO()
            {
                Id = branch.Id,
                Name = branch.Name,
                CreatedAt = branchDTO.CreatedAt,
                UpdatedAt = branchDTO.UpdatedAt,
                CompanyId = branch.CompanyId
            };

            return TypedResults.Ok(newBranchDTO);
        }





        [HttpGet("{id}")]
        public async Task<IResult> ReadBranch(Guid id)
        {
            BranchModel? branch;
            branch = await _db.Branch.FindAsync(id);

            if (branch == null)
                return TypedResults.NotFound(branch);

            var branchDTO = new BranchModelDTO()
            {
                Id = branch.Id,
                Name = branch.Name,
                CreatedAt = branch.CreatedAt,
                UpdatedAt = branch.UpdatedAt,
                CompanyId = branch.CompanyId,
                 
            };
            return TypedResults.Ok(branchDTO);
        }




        [HttpGet]
        public async Task<IResult> ReadAllBranches(MyDbContext _db)
        {
            var branchDTO = await _db.Branch.Select(branch => new BranchModelDTO()
            {

                Id = branch.Id, //?
                Name = branch.Name,
                CreatedAt = branch.CreatedAt,
                UpdatedAt = branch.UpdatedAt,
                CompanyId = branch.CompanyId

            }).ToListAsync();


            return TypedResults.Ok(branchDTO);

        }





        [HttpPut("{id}")]
        public async Task<IResult> UpdateBranch(Guid id, BranchModelDTO branchDTO)
        {
            var branch = await _db.Branch.FindAsync(id);

            if (branch == null)
                return TypedResults.NotFound(branch);


            branch.Name = branchDTO.Name;
            branch.CompanyId = branchDTO.CompanyId;

            var newBranchDTO = new BranchModelDTO()
            {
                Id = branch.Id,
                Name = branch.Name,
                CreatedAt = branch.CreatedAt,
                UpdatedAt = branch.UpdatedAt,
                CompanyId = branch.CompanyId,

            };

            await _db.SaveChangesAsync();

            return TypedResults.Ok(newBranchDTO);

        }




        [HttpDelete("{id}")]
        public async Task<IResult> DeleteBranch(Guid id)
        {
            var branch = await _db.Branch.FindAsync(id);

            if (branch == null)
                return TypedResults.NotFound();

            _db.Branch.Remove(branch);
            await _db.SaveChangesAsync();

            return TypedResults.NoContent();

        }




    }
}
