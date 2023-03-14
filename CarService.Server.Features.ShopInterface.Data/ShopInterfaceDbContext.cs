using CarService.Server.DbBuilder;
using CarService.Server.Features.ShopInterface.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Server.Features.ShopInterface.Data
{
    internal class ShopInterfaceDbContext : DbContext
    {
        public virtual DbSet<Step> Steps { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Step>(new Testing());
            modelBuilder.
        }

        private class Testing : IEntityTypeConfiguration<Step>
        {
            public void Configure(EntityTypeBuilder<Step> builder)
            {
                throw new NotImplementedException();
            }
        }
    }
}
