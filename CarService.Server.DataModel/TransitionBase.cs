using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Server.DataModel
{
    public class TransitionBase : Entity
    {
        [ForeignKey(nameof(SourceStepId))]
        public StepBase? SourceStep { get; protected set; }
        public int? SourceStepId { get; protected set; }

        [ForeignKey(nameof(TargetStepId))]
        public StepBase? TargetStep { get; protected set; }
        public int? TargetStepId { get; protected set; }
    }
}
