
namespace Company.Models
{
    public class BranchDTO
    {

        public Guid? Id { get; set; }
        public string Name { get; set; }
        public Guid CompanyId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }



     }
}
