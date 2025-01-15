namespace CompanyWork.Data
{
    public class EmployeePositionAsset
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid EmployeesToPositionId { get; set; }
        public Guid AssetId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime OwnedAssetFromDateTime { get; set; }
        public DateTime? OwnedAssetTillDateTime { get; set; }

    }
}
