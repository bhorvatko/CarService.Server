using CarService.Features.ShopInterface.Dto;
using CarService.Features.ShopInterface.Services.Events;
using CarService.Features.ShopInterface.Services.Projections;
using CarService.Server.Core.Events;
using CarService.Server.Domain.Model;
using CarService.Server.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Features.ShopInterface.Services.Services
{
    public class WarrantService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWarrantTypeRepository warrantTypes;
        private readonly IWarrantRepository warrants;
        private readonly ITechnicianRepository technicians;
        private readonly WarrantProjection warrantProjection;
        private readonly IEventDispatcher eventDispatcher;

        public WarrantService(IUnitOfWork unitOfWork, WarrantProjection warrantProjection, IEventDispatcher eventDispatcher)
        {
            this.unitOfWork = unitOfWork;
            this.warrantProjection = warrantProjection;
            this.eventDispatcher = eventDispatcher;

            this.warrantTypes = unitOfWork.WarrantTypes;
            this.warrants = unitOfWork.Warrants;
            this.technicians = unitOfWork.Technicians;
        }

        public async Task<WarrantDto> AddWarrant(DateTime deadline, int warrantTypeId, bool isUrgent, string subject)
        {
            Warrant domainModel = new Warrant(deadline, await warrantTypes.GetWarratTypeWithSteps(warrantTypeId), isUrgent, subject);

            await warrants.Add(domainModel);
            await unitOfWork.Save();

            WarrantDto warrantDto = warrantProjection.Project(domainModel);

            await eventDispatcher.DispatchEvent(new WarrantAddedEvent(warrantDto, null));

            return warrantDto;
        }

        public async Task<WarrantDto> GetWarrant(int id) 
            => await warrants.Get(id, warrantProjection.GetExpression());

        public async Task<IEnumerable<WarrantDto>> GetAllWarrants() 
            => await warrants.GetAll(warrantProjection.GetExpression());

        public async Task<IEnumerable<WarrantDto>> GetUnassignedWarrants() 
            => await warrants.Get(w => w.Technician == null, warrantProjection.GetExpression());


        public async Task<WarrantDto> UpdateWarrant(int id, DateTime deadline, int warrantTypeId, bool isUrgent, int currentStepId, string subject, IEnumerable<string> notes)
        {
            Warrant domainModel = await warrants.Get(id);
            domainModel.Update(deadline, await warrantTypes.GetWarratTypeWithSteps(warrantTypeId), isUrgent, subject, currentStepId, notes);

            await unitOfWork.Save();

            WarrantDto warrantDto = warrantProjection.Project(domainModel);

            await eventDispatcher.DispatchEvent(new WarrantUpdatedEvent(warrantDto));

            return warrantDto;
        }

        public async Task<int> DeleteWarrant(int id)
        {
            Technician? technician = await warrants.GetNullable(id, w => w.Technician);

            await warrants.Delete(id);

            await eventDispatcher.DispatchEvent(new WarrantRemovedEvent(id, technician?.Id));

            await unitOfWork.Save();

            return id;
        }

        public async Task<WarrantDto> AdvanceToNextStepAsync(int id)
        {
            Warrant domainModel = await warrants.GetWarrantWithWarrantType(id);

            domainModel.AdvanceToNextStep();

            await unitOfWork.Save();

            WarrantDto warrantDto = await warrants.Get(id, warrantProjection.GetExpression());

            await eventDispatcher.DispatchEvent(new WarrantUpdatedEvent(warrantDto));

            return warrantDto;
        }

        public async Task<WarrantDto> RollbackToPreviousStepAsync(int id)
        {
            Warrant domainModel = await warrants.GetWarrantWithWarrantType(id);

            domainModel.RollbackToPreviousStep();

            WarrantDto warrantDto = await warrants.Get(id, warrantProjection.GetExpression());

            await unitOfWork.Save();

            await eventDispatcher.DispatchEvent(new WarrantUpdatedEvent(warrantDto));

            return warrantDto;
        }

        public async Task<WarrantDto> AssignToTechnician(int warrantId, int? targetTechnicianId)
        {
            Warrant warrant = await warrants.Get(warrantId);
            int? removedFromTechnicianId = warrant.TechnicianId;

            Technician? targetTechnician = targetTechnicianId == null ?
                null :
                await technicians.Get(targetTechnicianId.Value);

            warrant.AssignToTechnician(targetTechnician);

            await unitOfWork.Save();

            WarrantDto warrantDto = await warrants.Get(warrantId, warrantProjection.GetExpression());

            await eventDispatcher.DispatchEvent(new WarrantAddedEvent(warrantDto, targetTechnicianId));
            await eventDispatcher.DispatchEvent(new WarrantRemovedEvent(warrantId, removedFromTechnicianId));

            return warrantDto;
        }
    }
}
