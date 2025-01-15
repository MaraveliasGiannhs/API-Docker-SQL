using Company.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CompanyWork.Data;
 


namespace CompanyWork.Controllers
{
    [ApiController]
    [Route("api/employeesToPositions")] 
    public class EmployeesToPositionController : ControllerBase
    {


        private readonly ILogger<EmployeesToPositionController> _logger;
        private readonly MyDbContext _db;

        public EmployeesToPositionController(ILogger<EmployeesToPositionController> logger, MyDbContext db)
        {
            _logger = logger;
            _db = db;
        }




        [HttpPost("/details")] //?
        public async Task<IResult> CreateDetails(EmployeesPositionsDTO etpDTO)
        {

            EmployeesToPositions etp = new()
            { 
                Id = Guid.NewGuid(),
                StartedWorkingAt = DateTime.Now,
                FinishedWorkingAt = DateTime.Now,  
                EmployeeId = etpDTO.EmployeeId,     
                WorkPositionId = etpDTO.WorkPositionId, 
            };

            await _db.EmployeesToPosition.AddAsync(etp);

            await _db.SaveChangesAsync();

            EmployeesPositionsDTO newEtpDTO = new()
            {
                Id = Guid.NewGuid(),
                StartedWorkingAt = DateTime.Now,
                FinishedWorkAt = DateTime.Now,
                EmployeeId = etp.EmployeeId,
                WorkPositionId = etp.WorkPositionId,
            };

            return TypedResults.Ok(newEtpDTO);
        }





        [HttpGet("/details/{id}")]
        public async Task<IResult> ReadDetails(Guid id)
        {

            EmployeesToPositions? etp = await _db.EmployeesToPosition.FindAsync(id);

            if (etp == null)
                return TypedResults.NotFound(etp);

            EmployeesPositionsDTO etpDTO = new()
            {
                Id = etp.Id,
                StartedWorkingAt = etp.StartedWorkingAt,
                FinishedWorkAt = etp.FinishedWorkingAt,
                EmployeeId = etp.EmployeeId,
                WorkPositionId = etp.WorkPositionId,

            };
            return TypedResults.Ok(etpDTO);
        }




        [HttpGet("/details")]
        public async Task<IResult> ReadAllDetails(MyDbContext _db)
        {
            var etpDTO= await _db.EmployeesToPosition.Select(etp => new EmployeesPositionsDTO()
            {

                Id = etp.Id,
                StartedWorkingAt = etp.StartedWorkingAt,
                FinishedWorkAt = etp.FinishedWorkingAt,
                EmployeeId = etp.EmployeeId,
                WorkPositionId = etp.WorkPositionId,

            }).ToListAsync();


            return TypedResults.Ok(etpDTO);

        }





        [HttpPut("/details/{id}")]
        public async Task<IResult> UpdateDetails(Guid id, EmployeesPositionsDTO etpDTO)
        {
            var etp = await _db.EmployeesToPosition.FindAsync(id);

            if (etp == null)
                return TypedResults.NotFound(etp);


            etp.Id = etpDTO.Id;
            etp.StartedWorkingAt = etpDTO.StartedWorkingAt;
            etp.FinishedWorkingAt = etpDTO.FinishedWorkAt;
            etp.EmployeeId = etpDTO.EmployeeId;
            etp.WorkPositionId = etpDTO.WorkPositionId;

            EmployeesPositionsDTO newEtpDTO = new()
            {
                Id = etp.Id,
                StartedWorkingAt = etp.StartedWorkingAt,
                FinishedWorkAt = etp.FinishedWorkingAt,
                EmployeeId = etp.EmployeeId,
                WorkPositionId = etp.WorkPositionId,
            };

            await _db.SaveChangesAsync();

            return TypedResults.Ok(newEtpDTO);

        }




        [HttpDelete("/details/{id}")]
        public async Task<IResult> DeleteDetails(Guid id)
        {
            var etp = await _db.EmployeesToPosition.FindAsync(id);

            if (etp == null)
                return TypedResults.NotFound();

            _db.EmployeesToPosition.Remove(etp);
            await _db.SaveChangesAsync();

            return TypedResults.NoContent();

        }
    }
}
