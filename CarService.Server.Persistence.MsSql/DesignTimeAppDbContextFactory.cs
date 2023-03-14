using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace CarService.Server.Persistence.MsSql
{
    internal class DesignTimeAppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=CS_dev;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=false");

            AppDbContext dbContext = new AppDbContext(optionsBuilder.Options);

            return dbContext;
        }
    }
}
