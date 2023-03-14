using CarService.Server.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Server.Domain.Repositories
{
    public interface ITechnicianRepository : IRepository<Technician>
    {
        Task<Technician> GetTechnicianWithWarrants(int id);
    }
}
