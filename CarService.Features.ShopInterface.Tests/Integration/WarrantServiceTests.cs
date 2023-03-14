using CarService.Features.ShopInterface.Dto;
using CarService.Features.ShopInterface.Services.Services;
using CarService.Features.ShopInterface.Tests.Integration.Factories;
using CarService.Features.ShopInterface.Tests.Integration.ServiceExtensions;
using CarService.Server.Domain.Model;
using CarService.Server.Persistence.MsSql;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Features.ShopInterface.Tests.Integration
{
    public class WarrantServiceTests : IntegrationTests
    {
        private WarrantService WarrantService => ServiceFactory.CreateWarrantService();
        private WarrantTypeService WarrantTypeService => ServiceFactory.CreateWarrantTypeService();
        private ProcedureService ProcedureService => ServiceFactory.CreateProcedureService();  
        private TechnicianService TechnicianService => ServiceFactory.CreateTechnicianService();

        [Fact]
        public async Task Creating_and_getting_a_warrant()
        {
            int warrantTypeId = await WarrantTypeService.CreateTestWarrantType(3);

            int warrantId = (await WarrantService.AddWarrant(DateTime.Now, warrantTypeId, false, "Subject")).Id;

            WarrantDto actual = await WarrantService.GetWarrant(warrantId);
            actual.WarrantType.Steps.Count().Equals(3);
            actual.CurrentStep.Should().BeEquivalentTo(actual.WarrantType.Steps.First());
        }

        [Fact]
        public async Task Updating_a_warrant()
        {
            int warrantTypeId = await WarrantTypeService.CreateTestWarrantType(3);
            int warrantId = (await WarrantService.AddWarrant(DateTime.Now, warrantTypeId, false, "Subject")).Id;
            WarrantTypeDto newWarrantType = await WarrantTypeService.AddWarrantType("Name", await ProcedureService.CreateTestProcedures(2));

            await WarrantService.UpdateWarrant(warrantId,
                DateTime.Now,
                newWarrantType.Id,
                false,
                newWarrantType.Steps.First().Id,
                "Subject",
                new List<string> { "Note1", "Note2" });

            WarrantDto actual = await WarrantService.GetWarrant(warrantId);
            actual.WarrantType.Steps.Count().Should().Be(2);
            actual.CurrentStep.Id.Should().Be(newWarrantType.Steps.First().Id);
            actual.Notes.Count().Should().Be(2);
        }

        [Fact]
        public async Task Deleting_a_warrant()
        {
            int warrantTypeId = await WarrantTypeService.CreateTestWarrantType(3);
            int warrantId = (await WarrantService.AddWarrant(DateTime.Now, warrantTypeId, false, "Subject")).Id;

            await WarrantService.DeleteWarrant(warrantId);
            
            (await WarrantService.GetAllWarrants()).Should().BeEmpty();
        }

        [Fact]
        public async Task Advancing_a_warrant_to_the_next_step()
        {
            WarrantTypeDto warrantType = await WarrantTypeService.AddWarrantType("Name", await ProcedureService.CreateTestProcedures(2));
            int warrantId = (await WarrantService.AddWarrant(DateTime.Now, warrantType.Id, false, "Subject")).Id;

            await WarrantService.AdvanceToNextStepAsync(warrantId);

            (await WarrantService.GetWarrant(warrantId)).CurrentStep.Id.Should().Be(warrantType.Steps.Last().Id);
        }

        [Fact]
        public async Task Rolling_a_warrant_back_to_the_previous_step()
        {
            WarrantTypeDto warrantType = await WarrantTypeService.AddWarrantType("Name", await ProcedureService.CreateTestProcedures(3));
            int warrantId = (await WarrantService.AddWarrant(DateTime.Now, warrantType.Id, false, "Subject")).Id;

            await WarrantService.AdvanceToNextStepAsync(warrantId);
            await WarrantService.AdvanceToNextStepAsync(warrantId);
            await WarrantService.RollbackToPreviousStepAsync(warrantId);

            (await WarrantService.GetWarrant(warrantId)).CurrentStep.Id.Should().Be(warrantType.Steps.ElementAt(1).Id);
        }

        [Fact]
        public async Task Reassigning_a_warrant_to_a_technician()
        {
            TechnicianDto firstTechnician = await TechnicianService.AddTechnician("First");
            TechnicianDto secondTechnician = await TechnicianService.AddTechnician("Second");
            int warrantTypeId = await WarrantTypeService.CreateTestWarrantType(3);
            WarrantDto warrant = await WarrantService.AddWarrant(DateTime.Now, warrantTypeId, false, "Subject");
            await WarrantService.AssignToTechnician(warrant.Id, firstTechnician.Id);

            await WarrantService.AssignToTechnician(warrant.Id, secondTechnician.Id);

            IEnumerable<TechnicianDto> technicians = await TechnicianService.GetAll();
            technicians.First().Warrants.Count().Should().Be(0);
            technicians.Last().Warrants.Count().Should().Be(1);
        }

        [Fact]
        public async Task Getting_unassigned_warrants()
        {
            TechnicianDto technician = await TechnicianService.AddTechnician("First");
            int warrantTypeId = await WarrantTypeService.CreateTestWarrantType(3);
            WarrantDto firstWarrant = await WarrantService.AddWarrant(DateTime.Now, warrantTypeId, false, "First");
            WarrantDto secondWarrant = await WarrantService.AddWarrant(DateTime.Now, warrantTypeId, false, "Second");
            await WarrantService.AssignToTechnician(firstWarrant.Id, technician.Id);
            IEnumerable<WarrantDto> expected = new List<WarrantDto>() { secondWarrant };

            IEnumerable<WarrantDto> actual = await WarrantService.GetUnassignedWarrants();

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task Unassigning_a_warrant()
        {
            TechnicianDto technician = await TechnicianService.AddTechnician("Name");
            int warrantTypeId = await WarrantTypeService.CreateTestWarrantType(3);
            WarrantDto warrant = await WarrantService.AddWarrant(DateTime.Now, warrantTypeId, false, "Subject");
            await WarrantService.AssignToTechnician(warrant.Id, technician.Id);

            await WarrantService.AssignToTechnician(warrant.Id, null);

            (await TechnicianService.GetAll()).First().Warrants.Should().BeEmpty();
        }
    }
}
