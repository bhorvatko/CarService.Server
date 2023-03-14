using CarService.Server.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace CarService.Server.DbBuilder
{
    internal class BuilderDbContext : DbContext
    {
        public virtual DbSet<ProcedureBase> Procedures { get; set; }
        public virtual DbSet<StepBase> Steps { get; set; }
        public virtual DbSet<TransitionBase> Transitions { get; set; }

        public BuilderDbContext(DbContextOptions options) : base(options) 
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration

            //foreach (IMutableEntityType entity in modelBuilder.Model.GetEntityTypes())
            //{
            //    modelBuilder.Entity(entity.ClrType).ToTable(entity.ClrType.Name.Replace("Base", ""));       
            //}
        }
    }
}