namespace Company.Models
{
    public class EmployeeDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime StartedWorkAt { get; set; }
        public DateTime FinishedWorkAt { get; set; }


    }
}
