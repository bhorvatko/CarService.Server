using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Server.Core.Projections
{
    public static class RegistrationProvider
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<ProjectionFactory>();
        }
    }
}
