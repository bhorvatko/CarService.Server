using CarService.Server.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Server.Features.ShopInterface.Model
{
    public class Step : StepBase
    {
        private Step() { }

        public Step(Procedure procedure)
        {
            Procedure = procedure;
        }
    }
}
