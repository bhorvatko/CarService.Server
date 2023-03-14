using CarService.Features.ShopInterface.Services.Services;
using CarService.Server.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Features.ShopInterface.Tests.Integration.ServiceExtensions
{
    internal static class ProcedureServiceExtensions
    {
        public async static Task<IEnumerable<int>> CreateTestProcedures(this ProcedureService service, int numberOfProcedures)
        {
            List<int> procedureIds = new List<int>();
            for (int i = 0; i < numberOfProcedures; i++)
            {
                procedureIds.Add((await service.AddProcedure("test", "000000")).Id);
            }

            return procedureIds;
        }
    }
}
