namespace Company.Data
{
    public class WorkingPositionModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid BranchId { get; set; }
    }
}
