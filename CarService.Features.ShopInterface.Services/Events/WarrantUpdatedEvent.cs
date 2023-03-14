using CarService.Features.ShopInterface.Dto;
using CarService.Server.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Features.ShopInterface.Services.Events
{
    public class WarrantUpdatedEvent : DomainEvent
    {
        public WarrantDto Warrant { get; private set; }

        public WarrantUpdatedEvent(WarrantDto warrant)
        {
            Warrant = warrant;
        }
    }
}
