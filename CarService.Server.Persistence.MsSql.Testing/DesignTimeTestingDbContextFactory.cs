using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Server.Persistence.MsSql.Testing
{
    internal class DesignTimeTestingDbContextFactory : IDesignTimeDbContextFactory<TestingDbContext>
    {
        public TestingDbContext CreateDbContext(string[] args)
        {
            return TestingDbContext.Create();
        }
    }
}
