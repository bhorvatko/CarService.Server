using CarService.Server.Domain.Model;
using CarService.Server.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Server.Persistence.MsSql.Repositories
{
    public class ProcedureRepository : Repository<Procedure>, IProcedureRepository
    {
        public ProcedureRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
