using CarService.Features.ShopInterface.Services.Projections;
using CarService.Features.ShopInterface.Services.Services;
using CarService.Server.Core.Projections.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Features.ShopInterface.Services
{
    public static class RegistrationProvider
    {
        public static void RegisterServices(IServiceCollection services, Func<IServiceProvider> serviceProvider)
        {
            services.AddProjection<ProcedureProjection>(serviceProvider);
            services.AddProjection<StepProjection>(serviceProvider);
            services.AddProjection<TechnicianProjection>(serviceProvider);
            services.AddProjection<TransitionProjection>(serviceProvider);
            services.AddProjection<WarrantProjection>(serviceProvider);
            services.AddProjection<WarrantTypeProjection>(serviceProvider);

            services.AddTransient<ProcedureService>();
            services.AddTransient<TechnicianService>();
            services.AddTransient<WarrantService>();
            services.AddTransient<WarrantTypeService>();
        }
    }
}
