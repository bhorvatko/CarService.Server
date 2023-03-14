using CarService.Server.Domain.Model;
using CarService.Server.Domain.Tests.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Server.Domain.Tests.Helpers
{
    internal static class WarrantHelper
    {
        public static Warrant CreateWarrant(DateTime deadline = default, 
            int numberOfSteps = 3, 
            bool isUrgent = false, 
            string subject = "test",
            int? id = null)
            => new Warrant(deadline, WarrantTypeHelper.CreateWarrantType(numberOfSteps), isUrgent, subject).SetId(id);
    }
}
