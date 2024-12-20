using Company.Data;
using Company.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.Controllers
{
    [ApiController]
    [Route("api/workingPositions")]
    public class WorkingPositionController : Controller
    {
        private readonly ILogger<WorkingPositionController> _logger;
        private readonly MyDbContext _db;

        public WorkingPositionController(ILogger<WorkingPositionController> logger, MyDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpPost("/workingPosition")]
        public async Task<IResult> CreateWorkingPos(WorkingPositionModelDTO workingPosDTO)
        {

            var workingPos = new WorkingPositionModel()
            {
                Id = Guid.NewGuid(),
                Name = workingPosDTO.Name,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,

            };

            await _db.WorkingPosition.AddAsync(workingPos);

            await _db.SaveChangesAsync();

            WorkingPositionModelDTO newWorkingPosDTO = new()
            {
                Id = workingPos.Id,
                Name = workingPos.Name,
                CreatedAt = workingPos.CreatedAt,
                UpdatedAt = workingPos.UpdatedAt,
            };

            return TypedResults.Ok(newWorkingPosDTO);
        }





        [HttpGet("/workingPosition/{id}")]
        public async Task<IResult> ReadWorkingPos(Guid id)
        {
            WorkingPositionModel? workingPos;
            workingPos = await _db.WorkingPosition.FindAsync(id);

            if (workingPos == null)
                return TypedResults.NotFound(workingPos);

            var workingPosDTO = new CompanyModelDTO()
            {
                Id = workingPos.Id,
                Name = workingPos.Name,
                CreatedAt = workingPos.CreatedAt,
                UpdatedAt = workingPos.UpdatedAt,
            };
            return TypedResults.Ok(workingPosDTO);
        }




        [HttpGet("/workingPosition")]
        public async Task<IResult> ReadAllWorkingPos(MyDbContext _db)
        {
            var workingPosDTO = await _db.WorkingPosition.Select(workingPos => new WorkingPositionModelDTO()
            {
                Id = workingPos.Id,
                Name = workingPos.Name,
                CreatedAt = workingPos.CreatedAt,
                UpdatedAt = workingPos.UpdatedAt,

            }).ToListAsync();

            return TypedResults.Ok(workingPosDTO);

        }





        [HttpPut("/workingPosition/{id}")]
        public async Task<IResult> UpdateWorkingPos(Guid id, WorkingPositionModelDTO workingPosDTO)
        {
            var workingPos = await _db.WorkingPosition.FindAsync(id);

            if (workingPos == null)
                return TypedResults.NotFound(workingPos);


            workingPos.Name = workingPosDTO.Name;
            workingPos.UpdatedAt = workingPosDTO.UpdatedAt;
            workingPos.CreatedAt = workingPosDTO.CreatedAt;

            WorkingPositionModelDTO newWorkingPosDTO= new()
            {
                Id = workingPos.Id,
                Name = workingPos.Name,
                CreatedAt = workingPos.CreatedAt,
                UpdatedAt = workingPos.UpdatedAt

            };

            await _db.SaveChangesAsync();

            return TypedResults.Ok(newWorkingPosDTO);

        }




        [HttpDelete("/workingPosition/{id}")]
        public async Task<IResult> DeleteWorkingPos(Guid id)
        {
            var workingPos = await _db.WorkingPosition.FindAsync(id);

            if (workingPos == null)
                return TypedResults.NotFound();

            _db.WorkingPosition.Remove(workingPos);
            await _db.SaveChangesAsync();

            return TypedResults.NoContent();

        }
    }
}
