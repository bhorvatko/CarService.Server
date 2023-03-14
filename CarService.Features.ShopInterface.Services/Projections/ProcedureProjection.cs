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
    public class ProcedureProjection : Projection<Procedure, ProcedureDto>
    {
        protected override Expression<Func<Procedure, ProcedureDto>> Expression => (p) => new ProcedureDto()
        {
            Id = p.Id,
            Name = p.Name,
            Color = p.Color,
            UsedByWarrantTypes = p.Steps.Select(s => s.WarrantType).Select(t => t.Name).ToList()
        };
    }
}
