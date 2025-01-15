using Microsoft.EntityFrameworkCore;

namespace CompanyWork.Data
{

    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { } //constructor

        public DbSet<Company> Company => Set<Company>(); //Company table
        public DbSet<Branch> Branch => Set<Branch>();
        public DbSet<WorkingPosition> WorkingPosition => Set<WorkingPosition>();
        public DbSet<Employee> Employee => Set<Employee>();
        public DbSet<EmployeesToPositions> EmployeesToPosition => Set<EmployeesToPositions>();
        public DbSet<Asset> Asset => Set<Asset>();
        public DbSet<EmployeePositionAsset> EmployeePositionAsset => Set<EmployeePositionAsset>();
        public DbSet<AssetType> AssetType => Set<AssetType>();


    }
}
