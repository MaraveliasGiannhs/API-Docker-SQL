namespace CompanyWork.PersistClasses
{
    public class AssetPersistDTO
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public Guid AssetTypeId { get; set; }
    }
}
