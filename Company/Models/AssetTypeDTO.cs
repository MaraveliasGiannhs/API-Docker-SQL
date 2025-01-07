namespace Company.Models
{
    public class AssetTypeDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }


    }

    public class AssetTypeLookup
    {
        public Guid? Id { get; set; }
        public string Like { get; set; }

    }
}
