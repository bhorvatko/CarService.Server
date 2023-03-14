using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Server.Domain.Repositories
{
    public class EntityNotFoundException<TEntity> : Exception
    {
        public EntityNotFoundException(int id) : base($"{typeof(TEntity).Name} with ID {id} not found.") { }
    }
}
