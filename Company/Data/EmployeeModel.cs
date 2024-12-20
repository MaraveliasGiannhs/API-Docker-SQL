namespace Company.Data
{
    public class EmployeeModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime StartedWorkAt { get; set; }
        public DateTime FinishedWorkAt { get; set; }


    }
}
