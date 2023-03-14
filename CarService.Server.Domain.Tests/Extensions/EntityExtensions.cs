using CarService.Server.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Server.Domain.Tests.Extensions
{
    internal static class EntityExtensions
    {
        public static TEntity SetId<TEntity>(this TEntity entity, int? id) where TEntity : Entity
        {
            if (id != null)
            {
                Type t = typeof(Entity);
                t.InvokeMember(nameof(Entity.Id), 
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.SetProperty | BindingFlags.Instance, 
                    null, 
                    entity, 
                    new object[] { id });
            }

            return entity;
        }
    }
}
