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
    public class WarrantTypeService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWarrantTypeRepository warrantTypes;
        private readonly IProcedureRepository procedures;
        private readonly WarrantTypeProjection warrantTypeProjection;

        public WarrantTypeService(IUnitOfWork unitOfWork, WarrantTypeProjection warrantTypeProjection)
        {
            this.unitOfWork = unitOfWork;
            this.warrantTypeProjection = warrantTypeProjection;

            this.warrantTypes = unitOfWork.WarrantTypes;
            this.procedures = unitOfWork.Procedures;
        }

        public async Task<WarrantTypeDto> AddWarrantType(string name, IEnumerable<int> procedureIds)
        {
            WarrantType warrantTypeDomainModel = new WarrantType(name, await procedures.Get(procedureIds));

            await warrantTypes.Add(warrantTypeDomainModel);
            await unitOfWork.Save();

            return warrantTypeProjection.Project(warrantTypeDomainModel);
        }

        public async Task<WarrantTypeDto> GetWarrantType(int id) 
            => await warrantTypes.Get(id, warrantTypeProjection.GetExpression());

        public async Task<IEnumerable<WarrantTypeDto>> GetAllWarrantTypes() 
            => await warrantTypes.GetAll(warrantTypeProjection.GetExpression());

        public async Task<WarrantTypeDto> UpdateWarrantType(int id, string name, IEnumerable<int> procedureIds)
        {
            WarrantType domainModel = await warrantTypes.Get(id);
            domainModel.Update(name, await procedures.Get(procedureIds));

            await unitOfWork.Save();

            return warrantTypeProjection.Project(domainModel);
        }

        public async Task<int> DeleteWarrantType(int id)
        {
            await warrantTypes.Delete(id);

            await unitOfWork.Save();

            return id;
        }
    }
}
