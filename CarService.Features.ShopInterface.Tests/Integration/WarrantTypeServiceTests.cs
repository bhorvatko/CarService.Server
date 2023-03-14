using CarService.Features.ShopInterface.Dto;
using CarService.Features.ShopInterface.Services.Services;
using CarService.Features.ShopInterface.Tests.Integration.Factories;
using CarService.Features.ShopInterface.Tests.Integration.ServiceExtensions;
using CarService.Server.Persistence.MsSql;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Features.ShopInterface.Tests.Integration
{
    public class WarrantTypeServiceTests : IntegrationTests
    {
        private WarrantTypeService WarrantTypeService => ServiceFactory.CreateWarrantTypeService();
        private ProcedureService ProcedureService => ServiceFactory.CreateProcedureService();

        [Fact]
        public async void Creating_and_getting_a_warrant_type()
        {
            IEnumerable<int> procedureIds = await ProcedureService.CreateTestProcedures(3);

            int warrantTypeId = (await WarrantTypeService.AddWarrantType("Test", procedureIds)).Id;

            WarrantTypeDto actual = await WarrantTypeService.GetWarrantType(warrantTypeId);
            actual.Steps.Count().Should().Be(3);
            actual.Steps.Select(s => s.BackTransition).Where(t => t != null).Count().Should().Be(2);
            actual.Steps.Select(s => s.ForwardTransition).Where(t => t != null).Count().Should().Be(2);
        }

        [Fact]
        public async void Updating_a_warrant_type()
        {
            IEnumerable<int> originalProcedureIds = await ProcedureService.CreateTestProcedures(3);
            int warrantTypeId = (await WarrantTypeService.AddWarrantType("Test", originalProcedureIds)).Id;
            IEnumerable<int> modifiedProcedureIds = await ProcedureService.CreateTestProcedures(2);

            await WarrantTypeService.UpdateWarrantType(warrantTypeId, "Modified", modifiedProcedureIds);

            WarrantTypeDto actual = await WarrantTypeService.GetWarrantType(warrantTypeId);
            actual.Steps.Count().Should().Be(2);
            actual.Steps.Select(s => s.BackTransition).Where(t => t != null).Count().Should().Be(1);
            actual.Steps.Select(s => s.ForwardTransition).Where(t => t != null).Count().Should().Be(1);
            actual.Name.Should().Be("Modified");
        }

        [Fact]
        public async void Deleting_a_warrant_type()
        {
            IEnumerable<int> originalProcedureIds = await ProcedureService.CreateTestProcedures(3);
            int warrantTypeId = (await WarrantTypeService.AddWarrantType("Test", originalProcedureIds)).Id;

            await WarrantTypeService.DeleteWarrantType(warrantTypeId);

            (await WarrantTypeService.GetAllWarrantTypes()).Count().Should().Be(0);
        }
    }
}
