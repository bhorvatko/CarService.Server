using Xunit;

namespace CarService.Server.Core.IntegrationTesting
{
    [Collection("Integration")]
    public abstract class IntegrationTests : IDisposable
    {
        public IntegrationTests()
        {
            DbContextFixture.DbContext.ClearDb();
        }

        public void Dispose()
        {
            DbContextFixture.DisposeDbContext();
        }
    }
}
