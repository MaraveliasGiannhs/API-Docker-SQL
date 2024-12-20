using System.ComponentModel.DataAnnotations.Schema;
using Company.Data;

namespace Company.Models
{
    public class BranchModelDTO
    {

        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CompanyId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }



     }
}
