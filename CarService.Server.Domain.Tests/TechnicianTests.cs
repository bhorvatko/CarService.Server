using CarService.Server.Domain.Model;
using CarService.Server.Domain.Tests.Helpers;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Server.Domain.Tests
{
    public class TechnicianTests
    {
        [Fact]
        public void Unassigning_warrants_sets_the_warrant_technician_to_none()
        {
            Warrant warrant = WarrantHelper.CreateWarrant(numberOfSteps: 1);
            Technician technician = new Technician("Name");
            warrant.AssignToTechnician(technician);

            technician.UnassignWarrants();

            warrant.Technician.Should().BeNull();
            warrant.TechnicianId.Should().BeNull();
        }
    }
}
