using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Features.ShopInterface.Dto
{
    public class StepDto
    {
        public int Id { get; set; }
        public ProcedureDto Procedure { get; set; } = null!;
        public TransitionDto? ForwardTransition { get; set; }
        public TransitionDto? BackTransition { get; set; }
    }
}
