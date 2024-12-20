namespace Company.Data
{
    public class BranchModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid CompanyId { get; set; }


    }
}
