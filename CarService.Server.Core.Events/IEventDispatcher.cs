using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Server.Core.Events
{
    public interface IEventDispatcher
    {
        Task DispatchEvent<TEvent>(TEvent message) where TEvent : DomainEvent;
    }
}
