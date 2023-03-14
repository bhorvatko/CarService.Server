using CarService.Features.ShopInterface.Dto;
using CarService.Features.ShopInterface.Services.Projections;
using CarService.Server.Domain.Model;
using CarService.Server.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Features.ShopInterface.Services.Services
{
    public class TechnicianService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ITechnicianRepository technicians;
        private readonly TechnicianProjection technicianProjection;

        public TechnicianService(IUnitOfWork unitOfWork, TechnicianProjection technicianProjection)
        {
            this.unitOfWork = unitOfWork;
            this.technicianProjection = technicianProjection;

            this.technicians = unitOfWork.Technicians;
        }

        public async Task<TechnicianDto> AddTechnician(string name)
        {
            Technician domainModel = new Technician(name);

            await technicians.Add(domainModel);
            await unitOfWork.Save();

            return technicianProjection.Project(domainModel);
        }

        public async Task<IEnumerable<TechnicianDto>> GetAll()
            => await technicians.GetAll(technicianProjection.GetExpression());


        public async Task<TechnicianDto> UpdateTechnician(int id, string name)
        {
            Technician domainModel = await technicians.Get(id);
            domainModel.Update(name);

            await unitOfWork.Save();

            return technicianProjection.Project(domainModel);
        }

        public async Task<int> DeleteTechnician(int id)
        {
            Technician technician = await technicians.GetTechnicianWithWarrants(id);

            technician.UnassignWarrants();

            await technicians.Delete(id);

            await unitOfWork.Save();

            return id;
        }
    }
}
