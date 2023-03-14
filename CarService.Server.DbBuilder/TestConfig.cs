using CarService.Server.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Server.DbBuilder
{
    public class TestConfig : IEntityTypeConfiguration<StepBase>
    {
        public void Configure(EntityTypeBuilder<StepBase> builder)
        {
            throw new NotImplementedException();
        }
    }
}
