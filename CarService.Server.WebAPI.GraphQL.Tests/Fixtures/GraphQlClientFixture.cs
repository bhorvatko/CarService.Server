using CarService.Server.Persistence.MsSql;
using CarService.Server.WebAPI.GraphQL.Tests.Helpers;
using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Server.WebAPI.GraphQL.Tests.Fixtures
{
    public class GraphQlClientFixture : IDisposable
    {
        public GraphQLHttpClient GraphQLClient { get; private set; }

        public GraphQlClientFixture()
        {
            WebApplicationFactory<Program> factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.Remove(services.Single(d => d.ServiceType == typeof(DbContextOptions<AppDbContext>)));

                    RegistrationProvider.RegisterServices(services, "Server=.\\SQLEXPRESS;Database=CS_integrationTesting;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=false");
                });
            });

            HttpClient httpClient = factory.CreateClient(new WebApplicationFactoryClientOptions());
            GraphQLClient = GraphQLHttpClientHelper.CreateGraphQLClient(httpClient);
        }

        public void Dispose()
        {
            GraphQLClient.Dispose();
        }
    }
}
