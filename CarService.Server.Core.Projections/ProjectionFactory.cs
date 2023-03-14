using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Server.Core.Projections
{
    public class ProjectionFactory
    {
        private static readonly Dictionary<Type, Func<IProjection>> factoryRegistrations = new Dictionary<Type, Func<IProjection>>();

        public static void RegisterFactory<TProjection>(Func<IProjection> factory) where TProjection : IProjection
        {
            factoryRegistrations.Add(typeof(TProjection), factory);
        }

        internal IProjection GetProjection(Type projectionType)
        {
            if (factoryRegistrations.TryGetValue(projectionType, out Func<IProjection>? factory))
            {
                return factory.Invoke();
            } else
            {
                throw new InvalidOperationException($"No registered projection of type {projectionType.GetType().Name}");
            }
        }
    }
}
