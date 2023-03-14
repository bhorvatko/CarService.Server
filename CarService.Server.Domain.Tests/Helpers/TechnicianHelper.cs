using CarService.Server.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Server.Domain.Tests.Helpers
{
    internal static class TechnicianHelper
    {
        public static Technician CreateTechnician(string name = "Test")
            => new Technician(name);
    }
}
