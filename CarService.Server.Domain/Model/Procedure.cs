using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Server.Domain.Model
{
    public class Procedure
    {
        public string Name { get; protected set; } = string.Empty;
        public string Color { get; protected set; } = string.Empty;
    }
}
