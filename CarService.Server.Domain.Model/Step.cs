using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Server.Domain.Model
{
    public class Step : Entity
    {
        public Procedure Procedure { get; protected set; }
        public Transition? ForwardTransition { get; set; }
        public Transition? BackTransition { get; set; }
        public WarrantType WarrantType { get; protected set; }

#nullable disable warnings
        private Step() { }
#nullable enable warnings

        public Step(Procedure procedure, WarrantType warrantType)
        {
            Procedure = procedure;
            WarrantType = warrantType;
        }
    }
}
