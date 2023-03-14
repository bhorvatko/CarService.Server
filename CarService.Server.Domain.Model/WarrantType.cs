using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Server.Domain.Model
{
    public class WarrantType : Entity
    {
        public string Name { get; protected set; }
        public IEnumerable<Step> Steps { get; private set; }

#nullable disable warnings
        private WarrantType() { }
#nullable enable warnings

        public WarrantType(string name, IEnumerable<Procedure> procedures)
        {
            Update(name, procedures);
        }

        [MemberNotNull(nameof(Name))]
        [MemberNotNull(nameof(Steps))]
        public void Update(string name, IEnumerable<Procedure> procedures)
        {
            if (!procedures.Any())
            {
                throw new ArgumentException("Warrant type must consist of at least 1 procedure.");
            }

            Name = name;
            Steps = new List<Step>();   

            foreach (Procedure procedure in procedures)
            {
                AddStep(new Step(procedure, this));
            }
        }

        private Step AddStep(Step nextStep)
        {
            List<Step> newSequence = new List<Step>(Steps);

            Step? lastStep = GetLastStep();

            if (lastStep != null)
            {
                Transition newTransition = new Transition(lastStep, nextStep);

                lastStep.ForwardTransition = newTransition;
                nextStep.BackTransition = newTransition;
            }

            newSequence.Add(nextStep);

            Steps = newSequence;
            return nextStep;
        }

        public Step GetInitialStep()
        {
            return Steps.First(s => s.BackTransition == null);
        }

        public Step? GetLastStep()
        {
            return Steps.FirstOrDefault(s => s.ForwardTransition == null);
        }
    }
}
