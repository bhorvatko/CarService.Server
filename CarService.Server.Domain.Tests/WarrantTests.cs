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
    public class WarrantTests
    {
        [Fact]
        public void Advancing_a_warrant_with_no_next_step_is_not_allowed()
        {
            Warrant warrant = WarrantHelper.CreateWarrant(numberOfSteps: 1);

            Action action = () => warrant.AdvanceToNextStep();

            action.Should().Throw<Exception>();
        }

        [Fact]
        public void Rolling_back_a_warrant_with_no_previous_step_is_not_allowed()
        {
            Warrant warrant = WarrantHelper.CreateWarrant(numberOfSteps: 1);

            Action action = () => warrant.RollbackToPreviousStep();

            action.Should().Throw<Exception>();
        }

        [Fact]
        public void Assigning_a_warrant_to_a_technician_adds_the_warrant_to_the_technicians_warrants()
        {
            Warrant warrant = WarrantHelper.CreateWarrant();
            Technician technician = TechnicianHelper.CreateTechnician();

            warrant.AssignToTechnician(technician);

            warrant.Technician.Should().NotBeNull();
            technician.Warrants.Should().BeEquivalentTo(new List<Warrant>() { warrant });
        }

        [Fact]
        public void Reassigning_warrant_removes_it_from_the_previous_technicians_warrants()
        {
            Warrant firstWarrant = WarrantHelper.CreateWarrant(id: 1);
            Warrant secondWarrant = WarrantHelper.CreateWarrant(id: 2);
            Technician technician = TechnicianHelper.CreateTechnician();
            firstWarrant.AssignToTechnician(technician);
            secondWarrant.AssignToTechnician(technician);

            firstWarrant.AssignToTechnician(null);

            technician.Warrants.Should().BeEquivalentTo(new List<Warrant>() { secondWarrant });
        }
    }
}
