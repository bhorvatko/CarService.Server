using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Server.Core.Events
{
    public abstract class DomainEvent
    {
        public string InitiatorSessionId { get; set; } = null!;
    }
}
