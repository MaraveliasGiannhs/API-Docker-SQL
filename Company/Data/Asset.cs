namespace CompanyWork.Data
{
    public class Asset
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid AssetTypeId { get; set; }

    }
}
