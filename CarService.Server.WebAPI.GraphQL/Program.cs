using CarService.Server.Core.Events;

namespace CarService.Server.WebAPI.GraphQL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            IServiceCollection services = builder.Services;
            ConfigurationManager configuration = builder.Configuration;
            WebApplication? app = null;

            Core.Projections.RegistrationProvider.RegisterServices(services);
            Persistence.MsSql.RegistrationProvider.RegisterServices(services, configuration.GetConnectionString("MsSql")!);
            Features.ShopInterface.Services.RegistrationProvider.RegisterServices(services, () => app!.Services);

            services.AddGraphQLServer()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .AddSubscriptionType<Subscription>()    
                .AddInMemorySubscriptions();
            services.AddHttpContextAccessor();
            services.AddTransient<IEventDispatcher, EventDispatcher>();
            
            app = builder.Build();

            app.UseRouting();
            app.UseWebSockets();
            app.MapGraphQL();

            app.Run();
        }
    }
}