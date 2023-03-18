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
    public class WarrantTypeTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(100)]
        public void Creating_a_warrant_type_creates_a_step_sequence_with_the_correct_number_of_steps(int numberOfSteps)
        {
            IEnumerable<Procedure> procedures = ProcedureHelper.CreateProcedures(numberOfSteps);

            WarrantType warrantType = new WarrantType("test", procedures);

            warrantType.Steps.Count().Should().Be(numberOfSteps);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(100)]
        public void Creating_a_warrant_type_creates_forward_transitions_for_all_steps_except_for_the_last_one(int numberOfSteps)
        {
            WarrantType warrantType = WarrantTypeHelper.CreateWarrantType(numberOfSteps);

            warrantType.Steps.Where(s => s.ForwardTransition != null).Count().Should().Be(numberOfSteps - 1);
            warrantType.Steps.Last().ForwardTransition.Should().Be(null);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(100)]
        public void Creating_a_warrant_type_creates_back_transitions_for_all_steps_except_for_the_first_one(int numberOfSteps)
        {
            WarrantType warrantType = WarrantTypeHelper.CreateWarrantType(numberOfSteps);

            warrantType.Steps.Where(s => s.BackTransition != null).Count().Should().Be(numberOfSteps - 1);
            warrantType.Steps.First().BackTransition.Should().Be(null);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(100)]
        public void Creating_a_warrant_type_creates_a_continous_step_sequence(int numberOfSteps)
        {
            WarrantType warrantType = WarrantTypeHelper.CreateWarrantType(numberOfSteps);

            TraverseStepSequence(warrantType.Steps).Should().Be(numberOfSteps);
        }

        [Fact]
        public void WarrantType_with_no_steps_is_valid()
        {
            IEnumerable<Procedure> procedures = new List<Procedure>();

            Action action = () => new WarrantType("test", procedures);

            action.Should().NotThrow<Exception>();
        }

        [Fact]
        public void Updating_warrant_type_replaces_steps_with_new_steps()
        {
            WarrantType warrantType = WarrantTypeHelper.CreateWarrantType(3);

            warrantType.Update("Test", ProcedureHelper.CreateProcedures(2));

            warrantType.Steps.Count().Should().Be(2);
        }

        private int TraverseStepSequence(IEnumerable<Step> steps)
        {
            int traversedSteps = 0;
            Step? nextStep = steps.FirstOrDefault();

            while (nextStep != null)
            {
                traversedSteps++;
                nextStep = nextStep.ForwardTransition?.TargetStep;
            }

            return traversedSteps;
        }
    }
}
