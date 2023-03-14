using CarService.Server.Domain.Model;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Server.Domain.Tests
{
    public class ProcedureTests
    {
        [Fact]
        public void Procedure_with_colorcode_in_nonhex_format_is_invalid()
        {
            Action action = () => new Procedure("test", "GFFFFF");

            action.Should().Throw<Exception>();
        }

        [Fact]
        public void Procedure_with_colorcode_out_of_range_is_invalid()
        {
            Action action = () => new Procedure("test", "1000000");

            action.Should().Throw<Exception>();
        }
    }
}
