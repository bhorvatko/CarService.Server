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
    public class ProcedureService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IProcedureRepository procedures;
        private readonly ProcedureProjection procedureProjection;

        public ProcedureService(IUnitOfWork unitOfWork, ProcedureProjection procedureProjection)
        {
            this.unitOfWork = unitOfWork;
            this.procedureProjection = procedureProjection;

            this.procedures = unitOfWork.Procedures;
        }

        public async Task<ProcedureDto> AddProcedure(string name, string color)
        {
            Procedure domainModel = new Procedure(name, color);

            await procedures.Add(domainModel);
            await unitOfWork.Save();

            return procedureProjection.Project(domainModel);
        }

        public async Task<ProcedureDto> GetProcedure(int id) 
            => await procedures.Get(id, procedureProjection.GetExpression());

        public async Task<IEnumerable<ProcedureDto>> GetAllProcedures()
            => await procedures.GetAll(procedureProjection.GetExpression());

        public async Task<ProcedureDto> UpdateProcedure(int id, string name, string color)
        {
            Procedure domainModel = await procedures.Get(id);
            domainModel.Update(name, color);

            await unitOfWork.Save();

            return procedureProjection.Project(domainModel);
        }

        public async Task<int> DeleteProcedure(int id)
        {
            await procedures.Delete(id);
            await unitOfWork.Save();

            return id;
        }
    }
}
