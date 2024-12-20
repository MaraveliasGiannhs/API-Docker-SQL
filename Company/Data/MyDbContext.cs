using Microsoft.EntityFrameworkCore;

namespace Company.Data
{

    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { } //constructor

        public DbSet<CompanyModel> Company => Set<CompanyModel>(); //Company table
        public DbSet<BranchModel> Branch => Set<BranchModel>();
        public DbSet<WorkingPositionModel> WorkingPosition => Set<WorkingPositionModel>();
        public DbSet<EmployeeModel> Employee => Set<EmployeeModel>();
        public DbSet<EmployeesToPositions> EmployeesToPosition => Set<EmployeesToPositions>();
        public DbSet<AssetModel> Asset => Set<AssetModel>();
        public DbSet<EmployeePositionAssetModel> EmployeePositionAsset => Set<EmployeePositionAssetModel>();
        public DbSet<AssetTypeModel> AssetType => Set<AssetTypeModel>();


    }
}
