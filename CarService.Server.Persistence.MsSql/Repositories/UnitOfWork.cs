using CarService.Server.Domain.Model;
using CarService.Server.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace CarService.Server.Persistence.MsSql.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext dbContext;

        public IProcedureRepository Procedures { get; private set; }
        public IWarrantTypeRepository WarrantTypes { get; private set; }
        public IWarrantRepository Warrants { get; private set; }
        public ITechnicianRepository Technicians { get; private set; }  

        public UnitOfWork(AppDbContext dbContext)
        {
            this.dbContext = dbContext;

            Procedures = new ProcedureRepository(dbContext);
            WarrantTypes = new WarrantTypeRepository(dbContext);
            Warrants = new WarrantRepository(dbContext);
            Technicians = new TechnicianRepository(dbContext);
        }

        public async Task Save() => await dbContext.SaveChangesAsync();

        public void Dispose() => dbContext.Dispose();
    }
}
