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
    public class TechnicianServiceTests : IntegrationTests
    {
        private TechnicianService TechnicianService => ServiceFactory.CreateTechnicianService();
        private WarrantService WarrantService => ServiceFactory.CreateWarrantService();

        [Fact]
        public async Task Creating_and_getting_all_technicians()
        {
            await TechnicianService.AddTechnician("Name");
            await TechnicianService.AddTechnician("Name");

            (await TechnicianService.GetAll()).Count().Should().Be(2);
        }

        [Fact]
        public async Task Updating_a_technician()
        {
            int technicianId = (await TechnicianService.AddTechnician("Name")).Id;

            await TechnicianService.UpdateTechnician(technicianId, "Updated");

            (await TechnicianService.GetAll()).First().Name.Should().Be("Updated");
        }

        [Fact]
        public async Task Deleting_a_technician()
        {
            int technicianId = (await TechnicianService.AddTechnician("Name")).Id;
            int warrantId = await WarrantService.CreateTestWarrant();
            await WarrantService.AssignToTechnician(warrantId, technicianId);

            await TechnicianService.DeleteTechnician(technicianId);

            (await WarrantService.GetUnassignedWarrants()).Count().Should().Be(1);
            (await TechnicianService.GetAll()).Should().BeEmpty();
        }
    }
}
