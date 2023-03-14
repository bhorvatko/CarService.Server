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
    public class TechnicianProjection : Projection<Technician, TechnicianDto>
    {
        private readonly WarrantProjection _warrantProjection;

        public TechnicianProjection(WarrantProjection warrantProjection)
        {
            _warrantProjection = warrantProjection;
        }

        protected override Expression<Func<Technician, TechnicianDto>> Expression => (t) => new TechnicianDto()
        {
            Id = t.Id,
            Name = t.Name,
            Warrants = t.Warrants.Select(w => _warrantProjection.Project(w))
        };
    }
}
