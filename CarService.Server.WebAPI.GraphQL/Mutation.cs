using CarService.Features.ShopInterface.Dto;
using CarService.Features.ShopInterface.Services.Services;
using HotChocolate.Subscriptions;

namespace CarService.Server.WebAPI.GraphQL
{
    public class Mutation
    {
        public async Task<ProcedureDto> AddProcedure([Service] ProcedureService procedureService, string name, string color)
            => await procedureService.AddProcedure(name, color);

        public async Task<ProcedureDto> UpdateProcedure([Service] ProcedureService procedureService, int id, string name, string color)
            => await procedureService.UpdateProcedure(id, name, color);

        public async Task<int> DeleteProcedure([Service] ProcedureService procedureService, int id)
            => await procedureService.DeleteProcedure(id);

        public async Task<WarrantTypeDto> AddWarrantType([Service] WarrantTypeService warrantTypeService, string name, IEnumerable<int> procedureIds)
            => await warrantTypeService.AddWarrantType(name, procedureIds);

        public async Task<WarrantTypeDto> UpdateWarrantType([Service] WarrantTypeService warrantTypeService, int id, string name, IEnumerable<int> procedureIds)
            => await warrantTypeService.UpdateWarrantType(id, name, procedureIds);

        public async Task<int> DeleteWarrantType([Service] WarrantTypeService warrantTypeService, int id)
        => await warrantTypeService.DeleteWarrantType(id);

        public async Task<WarrantDto> AddWarrant([Service] WarrantService warrantService,
            DateTime deadline, int warrantTypeId, bool isUrgent, string subject)
            => await warrantService.AddWarrant(deadline, warrantTypeId, isUrgent, subject);

        public async Task<WarrantDto> UpdateWarrant([Service] WarrantService warrantService,
            int id, DateTime deadline, int warrantTypeId, bool isUrgent, int currentStepId, string subject, IEnumerable<string> notes)
            => await warrantService.UpdateWarrant(id, deadline, warrantTypeId, isUrgent, currentStepId, subject, notes);

        public async Task<int> DeleteWarrant([Service] WarrantService warrantService, int id)
            => await warrantService.DeleteWarrant(id);

        public async Task<WarrantDto> AdvanceWarrantToNextStep([Service] WarrantService warrantService, int id)
            => await warrantService.AdvanceToNextStepAsync(id);

        public async Task<WarrantDto> RollbackWarrantToPreviousStep([Service] WarrantService warrantService, int id)
            => await warrantService.RollbackToPreviousStepAsync(id);

        public async Task<TechnicianDto> AddTechnician([Service] TechnicianService technicianService, string name)
            => await technicianService.AddTechnician(name);

        public async Task<TechnicianDto> UpdateTechnician([Service] TechnicianService technicianService, int id, string name)
            => await technicianService.UpdateTechnician(id, name);

        public async Task<int> DeleteTechnician([Service] TechnicianService technicianService, int id)
            => await technicianService.DeleteTechnician(id);

        public async Task<WarrantDto> AssignToTechnician([Service] WarrantService warrantService, int warrantId, int? technicianId)
            => await warrantService.AssignToTechnician(warrantId, technicianId);
    }
}
