using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Server.Domain.Repositories
{
    public interface IUnitOfWork
    {
        IProcedureRepository Procedures { get; }
        IWarrantTypeRepository WarrantTypes { get; }
        IWarrantRepository Warrants { get; }
        ITechnicianRepository Technicians { get; }

        Task Save();
    }
}
