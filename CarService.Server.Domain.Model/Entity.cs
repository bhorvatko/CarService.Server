using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Server.Domain.Model
{
    public abstract class Entity
    {
        [Key]
        public int Id { get; private set; } 
    }
}
