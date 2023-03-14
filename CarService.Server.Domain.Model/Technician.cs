using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CarService.Server.Domain.Model
{
    public class Technician : Entity
    {
        public string Name { get; protected set; }
        public IEnumerable<Warrant> Warrants { get; private set; } = new List<Warrant>();

#nullable disable warnings
        private Technician() { }
#nullable enable warnings

        public Technician(string name)
        {
            Update(name);
        }

        [MemberNotNull(nameof(Name))]
        public void Update(string name)
        {
            Name = name;
        }

        public void UnassignWarrants()
        {
            foreach (Warrant warrant in Warrants)
            {
                warrant.AssignToTechnician(null);
            }
        }

        internal void AddWarrant(Warrant warrant)
        {
            Warrants = Warrants.Append(warrant).ToList();
        }

        internal void RemoveWarrant(Warrant warrant)
        {
            Warrants = Warrants.Where(w => w.Id != warrant.Id).ToList();
        }
    }
}
