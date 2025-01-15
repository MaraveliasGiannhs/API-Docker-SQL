using Company.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CompanyWork.Data;
 


namespace CompanyWork.Controllers
{
    [ApiController]
    [Route("api/branches")] 
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
        public async Task<IResult> CreateBranch(BranchDTO branchDTO)
        {

            Branch branch = new()
            {
                Id = Guid.NewGuid(),
                Name = branchDTO.Name,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                CompanyId = branchDTO.CompanyId

            };

            await _db.Branch.AddAsync(branch);

            await _db.SaveChangesAsync();

            BranchDTO newBranchDTO = new()
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

            Branch? branch = await _db.Branch.FindAsync(id);

            if (branch == null)
                return TypedResults.NotFound(branch);

            BranchDTO branchDTO = new()
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
            var branchDTO = await _db.Branch.Select(branch => new BranchDTO()
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
        public async Task<IResult> UpdateBranch(Guid id, BranchDTO branchDTO)
        {
            var branch = await _db.Branch.FindAsync(id);

            if (branch == null)
                return TypedResults.NotFound(branch);


            branch.Name = branchDTO.Name;
            branch.CompanyId = branchDTO.CompanyId;

            BranchDTO newBranchDTO = new()
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
