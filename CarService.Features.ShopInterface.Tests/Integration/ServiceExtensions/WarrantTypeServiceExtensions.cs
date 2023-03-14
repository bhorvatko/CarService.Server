using CarService.Features.ShopInterface.Services.Services;
using CarService.Features.ShopInterface.Tests.Integration.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Features.ShopInterface.Tests.Integration.ServiceExtensions
{
    internal static class WarrantTypeServiceExtensions
    {
        public async static Task<int> CreateTestWarrantType(this WarrantTypeService service, int numberOfSteps)
            => (await service.AddWarrantType("Test", await ServiceFactory.CreateProcedureService().CreateTestProcedures(numberOfSteps))).Id;
    }
}
