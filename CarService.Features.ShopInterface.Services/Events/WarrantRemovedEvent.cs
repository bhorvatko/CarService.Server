using CarService.Server.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Features.ShopInterface.Services.Events
{
    public class WarrantRemovedEvent : DomainEvent
    {
        public int WarrantId { get; set; }
        public int? TechnicianId { get; set; }

        public WarrantRemovedEvent(int warrantId, int? technicianId)
        {
            WarrantId = warrantId;
            TechnicianId = technicianId;
        }
    }
}
