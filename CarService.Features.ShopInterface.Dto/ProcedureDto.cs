using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Features.ShopInterface.Dto
{
    public class ProcedureDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public IEnumerable<string> UsedByWarrantTypes { get; set; } = new List<string>();
    }
}
