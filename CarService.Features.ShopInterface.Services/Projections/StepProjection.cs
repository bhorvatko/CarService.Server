using CarService.Features.ShopInterface.Dto;
using CarService.Server.Core.Projections;
using CarService.Server.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Features.ShopInterface.Services.Projections
{
    public class StepProjection : Projection<Step, StepDto>
    {
        private readonly ProcedureProjection procedureProjection;
        private readonly TransitionProjection transitionProjection;

        public StepProjection(ProcedureProjection procedureProjection, TransitionProjection transitionProjection)
        {
            this.procedureProjection = procedureProjection;
            this.transitionProjection = transitionProjection;
        }

        protected override Expression<Func<Step, StepDto>> Expression => (s) => new StepDto()
        {
            Id = s.Id,
            Procedure = procedureProjection.Project(s.Procedure),
            BackTransition = s.BackTransition == null ? null : transitionProjection.Project(s.BackTransition),
            ForwardTransition = s.ForwardTransition == null ? null : transitionProjection.Project(s.ForwardTransition)
        };
    }
}
