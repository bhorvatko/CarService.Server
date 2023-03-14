using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Server.Domain.Model
{
    public class Transition : Entity
    {
        public int? SourceStepId { get; private set; }
        public int? TargetStepId { get; private set; }
        public Step? SourceStep { get; protected set; }
        public Step? TargetStep { get; protected set; }

#nullable disable warnings
        private Transition() { }
#nullable enable warnings

        public Transition(Step? sourceStep, Step? targetStep)
        {
            SourceStep = sourceStep;
            TargetStep = targetStep;
        }
    }
}
