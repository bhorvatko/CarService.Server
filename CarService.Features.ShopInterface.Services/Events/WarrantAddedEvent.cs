using CarService.Features.ShopInterface.Dto;
using CarService.Server.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Features.ShopInterface.Services.Events
{
    public class WarrantAddedEvent : DomainEvent
    {
        public WarrantDto Warrant { get; set; } = null!;
        public int? TechnicianId { get; set; }

        public WarrantAddedEvent(WarrantDto warrant, int? technicianId)
        {
            Warrant = warrant;
            TechnicianId = technicianId;
        }
    }
}
