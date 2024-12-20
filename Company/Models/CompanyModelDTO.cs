using Company.Data;

namespace Company.Models
{

    public class CompanyModelDTO 
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


    }
}
