using CarService.Features.ShopInterface.Dto;
using CarService.Features.ShopInterface.Services.Services;

namespace CarService.Server.WebAPI.GraphQL
{
    public class Query
    {
        public async Task<ProcedureDto> GetProcedure([Service] ProcedureService procedureService, int id)
            => await procedureService.GetProcedure(id);

        public async Task<IEnumerable<ProcedureDto>> GetAllProcedures([Service] ProcedureService procedureService)
            => await procedureService.GetAllProcedures();

        public async Task<WarrantTypeDto> GetWarrantType([Service] WarrantTypeService warrantTypeService, int id)
            => await warrantTypeService.GetWarrantType(id);

        public async Task<IEnumerable<WarrantTypeDto>> GetAllWarrantTypes([Service] WarrantTypeService warrantTypeService)
            => await warrantTypeService.GetAllWarrantTypes();

        public async Task<WarrantDto> GetWarrant([Service] WarrantService warrantService, int id)
            => await warrantService.GetWarrant(id);

        public async Task<IEnumerable<WarrantDto>> GetAllWarrants([Service] WarrantService warrantService)
            => await warrantService.GetAllWarrants();

        public async Task<IEnumerable<WarrantDto>> GetUnassignedWarrants([Service] WarrantService warrantService)
            => await warrantService.GetUnassignedWarrants();

        public async Task<IEnumerable<TechnicianDto>> GetAllTechnicians([Service] TechnicianService technicianService)
            => await technicianService.GetAll();
    }
}
