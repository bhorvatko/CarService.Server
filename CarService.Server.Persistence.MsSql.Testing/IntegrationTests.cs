using CarService.Server.Persistence.MsSql.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CarService.Server.Persistence.MsSql
{
    [Collection("Integration")]
    public abstract class IntegrationTests : IDisposable
    {
        public IntegrationTests()
        {
            TestingDbContext.DbContext.ClearDb();
        }

        public virtual void Dispose()
        {
            TestingDbContext.DisposeDbContext();
        }
    }
}
