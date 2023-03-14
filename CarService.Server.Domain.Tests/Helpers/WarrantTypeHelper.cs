using CarService.Server.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Server.Domain.Tests.Helpers
{
    internal static class WarrantTypeHelper
    {
        public static WarrantType CreateWarrantType(int numberOfStpes)
            => new WarrantType("test", ProcedureHelper.CreateProcedures(numberOfStpes));
    }
}
