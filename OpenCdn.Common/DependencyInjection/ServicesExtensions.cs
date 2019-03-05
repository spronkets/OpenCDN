using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace OpenCdn.Common.DependencyInjection
{
    public static class ServicesExtensions
    {
        /// <summary>
        /// This will auto-register any Transient, Scoped, and Singleton-annotated classes.
        /// </summary>
        /// <remarks>
        /// The largest scope (Singleton > Scoped > Transient) will override others.
        /// </remarks>
        public static Dictionary<Type, List<Type>> AddAny(this IServiceCollection services)
        {
            var registeredTypes = new Dictionary<Type, List<Type>>
            {
                [typeof(Transient)] = new List<Type>(),
                [typeof(Scoped)] = new List<Type>(),
                [typeof(Singleton)] = new List<Type>()
            };

            var assemblies = Assembly.GetCallingAssembly().GetReferencedAssemblies().Select(Assembly.Load);
            var types = assemblies.SelectMany(ra => ra.GetTypes());
            foreach (var type in types)
            {
                if (type.GetCustomAttributes(typeof(Singleton), true).Length > 0)
                {
                    registeredTypes[typeof(Singleton)].Add(type);
                    services.AddSingleton(type);
                }
                else if (type.GetCustomAttributes(typeof(Scoped), true).Length > 0)
                {
                    registeredTypes[typeof(Scoped)].Add(type);
                    services.AddScoped(type);
                }
                else if (type.GetCustomAttributes(typeof(Transient), true).Length > 0)
                {
                    registeredTypes[typeof(Transient)].Add(type);
                    services.AddScoped(type);
                }
            }
            return registeredTypes;
        }
    }
}
