using CarService.Features.ShopInterface.Dto;
using CarService.Server.Domain.Model;
using CarService.Server.Core.Projections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Features.ShopInterface.Services.Projections
{
    public class WarrantProjection : Projection<Warrant, WarrantDto>
    {
        private readonly WarrantTypeProjection warrantTypeProjection;
        private readonly StepProjection stepProjection;

        public WarrantProjection(WarrantTypeProjection warrantTypeProjection, StepProjection stepProjection)
        {
            this.warrantTypeProjection = warrantTypeProjection;
            this.stepProjection = stepProjection;
        }

        protected override Expression<Func<Warrant, WarrantDto>> Expression => (w) => new WarrantDto()
        {
            Id = w.Id,
            DeadLine = w.Deadline,
            WarrantType = warrantTypeProjection.Project(w.WarrantType),
            CurrentStep = stepProjection.Project(w.CurrentStep),
            IsUrgent = w.IsUrgent,
            Subject = w.Subject,
            Notes = w.Notes.Select(n => new NoteDto()
            {
                Id = n.Id,
                Content= n.Content,
                Created = n.Created
            })
        };
    }
}
