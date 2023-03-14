using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Features.ShopInterface.Dto
{
    public class TransitionDto
    {
        public int Id { get; set; }
        public int? SourceStepId { get; set; }
        public int? TargetStepId { get; set; }
    }
}
