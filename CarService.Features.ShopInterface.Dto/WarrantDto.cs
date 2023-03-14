using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Features.ShopInterface.Dto
{
    public class WarrantDto
    {
        public int Id { get; set; }
        public DateTime DeadLine { get; set; }
        public WarrantTypeDto WarrantType { get; set; } = null!;
        public StepDto CurrentStep { get; set; } = null!;
        public bool IsUrgent { get; set; }
        public string Subject { get; set; } = string.Empty;
        public IEnumerable<NoteDto> Notes { get; set; } = new List<NoteDto>();
    }
}
