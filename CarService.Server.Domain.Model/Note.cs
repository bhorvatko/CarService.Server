using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Server.Domain.Model
{
    public class Note : Entity
    {
        public string Content { get; protected set; }
        public DateTime Created { get; protected set; }

#nullable disable warnings
        private Note() { }
#nullable enable warnings

        public Note(string content)
        {
            Content = content;
            Created = DateTime.Now;
        }
    }
}
