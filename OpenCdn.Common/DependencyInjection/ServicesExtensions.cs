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
            var implementationTypes = assemblies.SelectMany(ra => ra.GetTypes());
            foreach (var implementationType in implementationTypes)
            {
                if (implementationType.GetCustomAttributes(typeof(Singleton), true).Length > 0)
                {
                    var serviceTypes = implementationType.GetInterfaces();
                    if (serviceTypes.Length > 0)
                    {
                        foreach (var serviceType in serviceTypes)
                        {
                            registeredTypes[typeof(Singleton)].Add(serviceType);
                            services.AddSingleton(serviceType, implementationType);
                        }
                    }
                    else
                    {
                        registeredTypes[typeof(Singleton)].Add(implementationType);
                        services.AddSingleton(implementationType);
                    }
                }
                else if (implementationType.GetCustomAttributes(typeof(Scoped), true).Length > 0)
                {
                    var serviceTypes = implementationType.GetInterfaces();
                    if (serviceTypes.Length > 0)
                    {
                        foreach (var serviceType in serviceTypes)
                        {
                            registeredTypes[typeof(Scoped)].Add(serviceType);
                            services.AddScoped(serviceType, implementationType);
                        }
                    }
                    else
                    {
                        registeredTypes[typeof(Scoped)].Add(implementationType);
                        services.AddScoped(implementationType);
                    }
                }
                else if (implementationType.GetCustomAttributes(typeof(Transient), true).Length > 0)
                {
                    var serviceTypes = implementationType.GetInterfaces();
                    if (serviceTypes.Length > 0)
                    {
                        foreach (var serviceType in serviceTypes)
                        {
                            registeredTypes[typeof(Transient)].Add(serviceType);
                            services.AddTransient(serviceType, implementationType);
                        }
                    }
                    else
                    {
                        registeredTypes[typeof(Transient)].Add(implementationType);
                        services.AddTransient(implementationType);
                    }
                }
            }
            return registeredTypes;
        }
    }
}
