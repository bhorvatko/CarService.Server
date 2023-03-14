using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Server.Persistence.MsSql.Testing
{
    public class TestingDbContext : AppDbContext
    {
        private static TestingDbContext? instance;
        public static TestingDbContext DbContext => instance ??= Create();

        private TestingDbContext(DbContextOptions options) : base(options)
        {
        }

        public static TestingDbContext Create()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TestingDbContext>();
            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=CS_integrationTesting;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=false");

            return new TestingDbContext(optionsBuilder.Options);
        }

        public void ClearDb()
        {
            Database.ExecuteSqlRaw("DELETE FROM dbo.Transitions");
            Database.ExecuteSqlRaw("DELETE FROM dbo.Steps");
            Database.ExecuteSqlRaw("DELETE FROM dbo.WarrantTypes");
            Database.ExecuteSqlRaw("DELETE FROM dbo.Procedures");
            Database.ExecuteSqlRaw("DELETE FROM dbo.Warrants");
            Database.ExecuteSqlRaw("DELETE FROM dbo.Technicians");
        }

        public static void DisposeDbContext()
        {
            instance?.Dispose();
            instance = null;
        }
    }
}
