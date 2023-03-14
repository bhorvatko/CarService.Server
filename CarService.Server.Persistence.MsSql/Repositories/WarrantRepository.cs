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
    public class WarrantRepository : Repository<Warrant>, IWarrantRepository
    {
        public WarrantRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Warrant> GetWarrantWithWarrantType(int id)
            => await GetWithQueryable(id, q => q
                .Include(w => w.WarrantType).ThenInclude(wt => wt.Steps).ThenInclude(s => s.Procedure)
                .Include(w => w.WarrantType).ThenInclude(wt => wt.Steps).ThenInclude(s => s.ForwardTransition)
                .Include(w => w.WarrantType).ThenInclude(wt => wt.Steps).ThenInclude(s => s.BackTransition));
    }
}
