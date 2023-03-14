using CarService.Features.ShopInterface.Services.Services;
using CarService.Server.Core.Events;
using CarService.Server.Domain.Repositories;
using CarService.Server.Persistence.MsSql.Repositories;
using CarService.Server.Persistence.MsSql.Testing;
using Moq;

namespace CarService.Features.ShopInterface.Tests.Integration.Factories
{
    internal static class ServiceFactory
    {

        public static ProcedureService CreateProcedureService()
            => new ProcedureService(CreateUnitOfWork(), TestProjectionFactory.CreateProcedureProjection());

        public static WarrantTypeService CreateWarrantTypeService()
            => new WarrantTypeService(CreateUnitOfWork(), TestProjectionFactory.CreateWarrantTypeProjection());

        public static WarrantService CreateWarrantService()
            => new WarrantService(CreateUnitOfWork(), TestProjectionFactory.CreateWarrantProjection(), Mock.Of<IEventDispatcher>());

        public static TechnicianService CreateTechnicianService()
            => new TechnicianService(CreateUnitOfWork(), TestProjectionFactory.CreateTechnicianProjection());

        private static IUnitOfWork CreateUnitOfWork()
            => new UnitOfWork(TestingDbContext.DbContext);
    }
}
