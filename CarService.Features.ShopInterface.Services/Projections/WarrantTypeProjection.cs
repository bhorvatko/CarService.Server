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
    public class WarrantTypeProjection : Projection<WarrantType, WarrantTypeDto>
    {
        private readonly StepProjection stepProjection;

        public WarrantTypeProjection(StepProjection stepProjection)
        {
            this.stepProjection = stepProjection;
        }

        protected override Expression<Func<WarrantType, WarrantTypeDto>> Expression => (wt) => new WarrantTypeDto()
        {
            Id = wt.Id,
            Name = wt.Name,
            Steps = wt.Steps.Select(s => stepProjection.Project(s))
        };
    }
}
