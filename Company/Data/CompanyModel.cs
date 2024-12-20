using System.Text.Json.Serialization;

namespace Company.Data
{
    public class CompanyModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
