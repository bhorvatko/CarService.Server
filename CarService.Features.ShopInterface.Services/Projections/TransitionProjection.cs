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
    public class TransitionProjection : Projection<Transition, TransitionDto>
    {
        protected override Expression<Func<Transition, TransitionDto>> Expression => (t) => new TransitionDto()
        {
            Id = t.Id,
            SourceStepId = t.SourceStep == null ? null : t.SourceStep.Id,
            TargetStepId = t.TargetStep == null ? null : t.TargetStep.Id
        };
    }
}
