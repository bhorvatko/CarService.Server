using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Server.DataModel
{
    public class StepBase : Entity
    {
        [ForeignKey(nameof(ProcedureId))]
        public ProcedureBase Procedure { get; protected set; } = null!;
        public int ProcedureId { get; protected set; }

        [ForeignKey(nameof(TransitionBase.SourceStepId))]
        public TransitionBase? ForwardTransition { get; protected set; }
        //public int? ForwardTransitionId { get; protected set; }

        [ForeignKey(nameof(TransitionBase.TargetStepId))]
        public TransitionBase? BackTransition { get; protected set; }
        //public int? BackTransitionId { get; protected set; }
    }
}
