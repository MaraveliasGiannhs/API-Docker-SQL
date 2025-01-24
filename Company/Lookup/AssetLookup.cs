namespace CompanyWork.Lookup
{
    public class AssetLookup
    {
        public Guid? Id { get; set; }
        public string Like { get; set; }
        public int? PageIndex { get; set; }
        public int? ItemsPerPage { get; set; }
        public string? OrderItem { get; set; }
        public bool? AscendingOrder { get; set; }

        //add paging and ordering ?

    }
}
