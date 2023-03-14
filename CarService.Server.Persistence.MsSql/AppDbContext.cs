using CarService.Server.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Server.Persistence.MsSql
{
    public class AppDbContext : DbContext
    {
        public DbSet<Note> Notes { get; protected set; }
        public DbSet<Procedure> Procedures { get; protected set; }
        public DbSet<Step> Steps { get; protected set; }
        public DbSet<Technician> Technicians { get; protected set; }
        public DbSet<Transition> Transitions { get; protected set; }
        public DbSet<Warrant> Warrants { get; protected set; }
        public DbSet<WarrantType> WarrantTypes { get; protected set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Step>(e =>
            {
                e.HasOne(e => e.BackTransition).WithOne(e => e.TargetStep).HasForeignKey<Transition>(e => e.TargetStepId).OnDelete(DeleteBehavior.ClientCascade);
                e.HasOne(e => e.ForwardTransition).WithOne(e => e.SourceStep).HasForeignKey<Transition>(e => e.SourceStepId).OnDelete(DeleteBehavior.ClientCascade);
            });

            modelBuilder.Entity<WarrantType>(e =>
            {
                e.HasMany(e => e.Steps).WithOne(e => e.WarrantType).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Warrant>(e =>
            {
                e.HasOne(w => w.WarrantType).WithMany().OnDelete(DeleteBehavior.Restrict);
                e.HasMany(w => w.Notes).WithOne().OnDelete(DeleteBehavior.Cascade);
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(new LoggerFactory(new[] 
            {
                new DebugLoggerProvider()
            }));
        }
    }
}
