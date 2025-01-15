namespace CompanyWork.Data
{
    public class EmployeesToPositions
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid WorkPositionId { get; set; }
        public DateTime StartedWorkingAt { get; set; }
        public DateTime FinishedWorkingAt { get; set; }
         

    }
}
