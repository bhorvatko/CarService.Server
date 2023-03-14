using CarService.Server.Domain.Model;
using CarService.Server.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Server.Persistence.MsSql.Repositories
{
    public class TechnicianRepository : Repository<Technician>, ITechnicianRepository
    {
        public TechnicianRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Technician> GetTechnicianWithWarrants(int id)
            => await GetWithQueryable(id, q => q.Include(t => t.Warrants));
    }
}
