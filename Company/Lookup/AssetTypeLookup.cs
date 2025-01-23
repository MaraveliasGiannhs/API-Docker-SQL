namespace CompanyWork.Lookup
{
    public class AssetTypeLookup
    {
        public Guid? Id { get; set; }
        public string? Like { get; set; }
        public int? PageIndex { get; set; }
        public int? ItemsPerPage { get; set; }
        public string? OrderItem { get; set; }
        public bool? AscendingOrder { get; set; }
    }
}
