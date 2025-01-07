using Company.Data;
using Company.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.Controllers
{
    [ApiController]
    [Route("api/employee")] //?
    public class EmployeeController : ControllerBase
    {


        private readonly ILogger<EmployeeController> _logger;
        private readonly MyDbContext _db;

        public EmployeeController(ILogger<EmployeeController> logger, MyDbContext db)
        {
            _logger = logger;
            _db = db;
        }




        [HttpPost] 
        public async Task<IResult> CreateEmployee(EmployeeModelDTO employeeDTO)
        {

            var employee = new EmployeeModel()
            {
                Id = Guid.NewGuid(),
                Name = employeeDTO.Name,
                StartedWorkAt = DateTime.UtcNow,
                FinishedWorkAt = DateTime.UtcNow,
                //

            };

            await _db.Employee.AddAsync(employee);

            await _db.SaveChangesAsync();

            var newEmployeeDTO = new EmployeeModelDTO()
            {
                Id = employee.Id,
                Name = employee.Name,
                StartedWorkAt = employee.StartedWorkAt,
                FinishedWorkAt = employee.FinishedWorkAt,
                //
            };

            return TypedResults.Ok(newEmployeeDTO);
        }





        [HttpGet("{id}")]
        public async Task<IResult> ReadEmployee(Guid id)
        {
            EmployeeModel? employee;
            employee = await _db.Employee.FindAsync(id);

            if (employee == null)
                return TypedResults.NotFound(employee);

            var employeeDTO = new EmployeeModelDTO()
            {
                Id = employee.Id,
                Name = employee.Name,
                StartedWorkAt = employee.StartedWorkAt,
                FinishedWorkAt = employee.FinishedWorkAt,
                 
            };
            return TypedResults.Ok(employeeDTO);
        }




        [HttpGet]
        public async Task<IResult> ReadAllEmployees(MyDbContext _db)
        {
            var employeeDTO = await _db.Employee.Select(employee => new EmployeeModelDTO()
            {

                Id = employee.Id, //?
                Name = employee.Name,
                StartedWorkAt = employee.StartedWorkAt,
                FinishedWorkAt = employee.FinishedWorkAt,
                //
            }).ToListAsync();


            return TypedResults.Ok(employeeDTO);

        }





        [HttpPut("{id}")]
        public async Task<IResult> UpdateEmployee(Guid id, EmployeeModelDTO employeeDTO)
        {
            var employee = await _db.Employee.FindAsync(id);

            if (employee == null)
                return TypedResults.NotFound(employee);
            
            //fields taken from frontend
            employee.Name = employeeDTO.Name;
            //employee.StartedWorkAt = employeeDTO.StartedWorkAt;
            employee.FinishedWorkAt = employeeDTO.FinishedWorkAt;
            //Also change WorkingPos here

            var newEmployeeDTO = new EmployeeModelDTO()
            {
                Id = employee.Id,
                Name = employee.Name,
                StartedWorkAt = employee.StartedWorkAt,
                FinishedWorkAt = employee.FinishedWorkAt,
                //

            };

            await _db.SaveChangesAsync();

            return TypedResults.Ok(newEmployeeDTO);

        }




        [HttpDelete("{id}")]
        public async Task<IResult> DeleteEmployee(Guid id)
        {
            var employee = await _db.Employee.FindAsync(id);

            if (employee== null)
                return TypedResults.NotFound();

            _db.Employee.Remove(employee);
            await _db.SaveChangesAsync();

            return TypedResults.NoContent();

        }




    }
}
