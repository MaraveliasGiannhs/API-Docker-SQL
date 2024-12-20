namespace Company.Models
{
    public class EmployeesPositionsDTO
    {
        public Guid Id { get; set; }
        public DateTime StartedWorkingAt { get; set; }
        public DateTime FinishedWorkAt { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid WorkPositionId { get; set; }
    }
}
