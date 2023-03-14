using CarService.Server.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Server.Domain.Tests.Helpers
{
    internal static class ProcedureHelper
    {
        public static IEnumerable<Procedure> CreateProcedures(int numberOfProcedures)
            => Enumerable.Range(0, numberOfProcedures).Select(x => new Procedure("Test", "0")).ToList();
    }
}
