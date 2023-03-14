using CarService.Features.ShopInterface.Services.Projections;
using CarService.Server.Core.Projections;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Features.ShopInterface.Tests.Integration.Factories
{
    internal static class TestProjectionFactory
    {
        static TestProjectionFactory()
        {
            ProjectionFactory.RegisterFactory<ProcedureProjection>(CreateProcedureProjection);
            ProjectionFactory.RegisterFactory<TransitionProjection>(CreateTransitionProjection);
            ProjectionFactory.RegisterFactory<StepProjection>(CreateStepProjection);
            ProjectionFactory.RegisterFactory<WarrantTypeProjection>(CreateWarrantTypeProjection);
            ProjectionFactory.RegisterFactory<WarrantProjection>(CreateWarrantProjection);
            ProjectionFactory.RegisterFactory<TechnicianProjection>(CreateTechnicianProjection);
        }


        public static ProcedureProjection CreateProcedureProjection() =>
            new ProcedureProjection();

        public static TransitionProjection CreateTransitionProjection() =>
            new TransitionProjection();

        public static StepProjection CreateStepProjection()
            => new StepProjection(CreateProcedureProjection(), CreateTransitionProjection());

        public static WarrantTypeProjection CreateWarrantTypeProjection()
            => new WarrantTypeProjection(CreateStepProjection());

        public static WarrantProjection CreateWarrantProjection()
            => new WarrantProjection(CreateWarrantTypeProjection(), CreateStepProjection());

        public static TechnicianProjection CreateTechnicianProjection()
            => new TechnicianProjection(CreateWarrantProjection());
    }
}
