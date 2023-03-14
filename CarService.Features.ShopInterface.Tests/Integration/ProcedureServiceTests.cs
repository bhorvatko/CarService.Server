using CarService.Features.ShopInterface.Dto;
using CarService.Features.ShopInterface.Services.Projections;
using CarService.Features.ShopInterface.Services.Services;
using CarService.Features.ShopInterface.Tests.Integration.Factories;
using CarService.Server.Domain.Model;
using CarService.Server.Persistence.MsSql;
using CarService.Server.Persistence.MsSql.Testing;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Features.ShopInterface.Tests.Integration
{
    public class ProcedureServiceTests : IntegrationTests
    {
        private ProcedureService ProcedureService => ServiceFactory.CreateProcedureService();

        [Fact]
        public async Task Adding_and_getting_a_procedure()
        {
            ProcedureDto expectedResult = CreateProcedureDto("Test", "FFFFFF");

            ProcedureDto response = await ProcedureService.AddProcedure("Test", "FFFFFF");
            ProcedureDto actual = await ProcedureService.GetProcedure(response.Id);

            actual.Should().BeEquivalentTo(expectedResult, opt => opt.Excluding(p => p.Id));
        }

        [Fact]
        public async Task Getting_all_procedures()
        {
            await ProcedureService.AddProcedure("Test1", "000000");
            await ProcedureService.AddProcedure("Test2", "FFFFFF");

            IEnumerable<ProcedureDto> actual = await ProcedureService.GetAllProcedures();

            actual.Count().Should().Be(2);
        }

        [Fact]
        public async Task Updating_a_procedure()
        {
            ProcedureDto createdProcedure = await ProcedureService.AddProcedure("Test1", "FFFFFF");
            ProcedureDto expectedResult = CreateProcedureDto("Test2", "000000", createdProcedure.Id);
            
            await ProcedureService.UpdateProcedure(createdProcedure.Id, "Test2", "000000");

            ProcedureDto actual = await ProcedureService.GetProcedure(createdProcedure.Id);
            actual.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task Deleting_a_procedure()
        {
            ProcedureDto createdProcedure = await ProcedureService.AddProcedure("Test1", "FFFFFF");
            
            await ProcedureService.DeleteProcedure(createdProcedure.Id);

            (await ProcedureService.GetAllProcedures()).Should().BeEmpty();
        }

        private static ProcedureDto CreateProcedureDto(string name = "Test", string color = "FFFFFF", int id = 0)
            => new ProcedureDto() { Name = name, Color = color, Id = id, UsedByWarrantTypes = new string[] { } };
    }
}
