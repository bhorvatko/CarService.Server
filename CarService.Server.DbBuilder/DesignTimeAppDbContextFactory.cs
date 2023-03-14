using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Server.DbBuilder
{
    internal class DesignTimeAppDbContextFactory : IDesignTimeDbContextFactory<BuilderDbContext>
    {
        public BuilderDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BuilderDbContext>();
            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=CS_Test;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=false");

            BuilderDbContext dbContext = new BuilderDbContext(optionsBuilder.Options);

            return dbContext;
        }
    }
}
