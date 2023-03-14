using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Server.Core.Projections.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddProjection<TProjection>(this IServiceCollection services, Func<IServiceProvider> serviceProvider) where TProjection : class, IProjection
        {
            services.AddTransient<TProjection>();
            ProjectionFactory.RegisterFactory<TProjection>(() => serviceProvider.Invoke().GetService<TProjection>()!);
        }
    }
}
