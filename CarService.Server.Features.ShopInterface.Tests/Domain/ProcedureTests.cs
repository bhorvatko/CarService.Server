using CarService.Server.Features.ShopInterface.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Server.Features.ShopInterface.Tests.Domain
{
    public class ProcedureTests
    {
        [Fact]
        public void Procedure_with_colorcode_in_nonhex_format_is_invalid()
        {
            Action procedureFactory = () => new Procedure("test", "GFFFFF");

            Assert.ThrowsAny<Exception>(procedureFactory);
        }

        [Fact]
        public void Procedure_with_colorcode_out_of_range_is_invalid()
        {
            Action procedureFactory = () => new Procedure("test", "1000000");

            Assert.ThrowsAny<Exception>(procedureFactory);
        }


    }
}
