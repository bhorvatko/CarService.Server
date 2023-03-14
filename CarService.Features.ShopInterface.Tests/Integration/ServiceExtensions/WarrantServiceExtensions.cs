using CarService.Features.ShopInterface.Services.Services;
using CarService.Features.ShopInterface.Tests.Integration.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Features.ShopInterface.Tests.Integration.ServiceExtensions
{
    internal static class WarrantServiceExtensions
    {
        public static async Task<int> CreateTestWarrant(this WarrantService warrantService)
            => (await warrantService.AddWarrant(DateTime.Now, await ServiceFactory.CreateWarrantTypeService().CreateTestWarrantType(3), false, "Subject")).Id;
    }
}
