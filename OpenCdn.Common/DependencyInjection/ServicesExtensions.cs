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
        /// This will auto-register any Transient, Scoped, and Singleton-attributed interfaces/classes.
        /// </summary>
        /// <remarks>
        /// The largest scope (Singleton > Scoped > Transient) will override others.
        /// </remarks>
        public static Dictionary<Type, Type> AutoRegisterDependencies(this IServiceCollection services)
        {
            var attributes = new List<Type> { typeof(Transient), typeof(Scoped), typeof(Singleton) };
            var assemblyTypes = Assembly.GetCallingAssembly().GetReferencedAssemblies().Select(Assembly.Load).SelectMany(a => a.GetTypes()).ToList();

            var registrations = new Dictionary<Type, Type>();
            foreach (var attribute in attributes)
            {
                var attributedTypes = assemblyTypes.Where(at => at.CustomAttributes.Any(ata => ata.AttributeType == attribute)).ToList();
                foreach (var attributedType in attributedTypes)
                {
                    Type implementedType;
                    if (attributedType.IsClass)
                    {
                        implementedType = attributedType;
                    }
                    else if (attributedType.IsInterface)
                    {
                        var implementedTypes = assemblyTypes.Where(at => attributedType.IsAssignableFrom(at) && at != attributedType).ToList();
                        if (implementedTypes.Count != 1)
                        {
                            throw new NotSupportedException($"{attributedType.FullName} is not implemented properly for dependency auto registration.");
                        }

                        implementedType = implementedTypes[0];
                    }
                    else
                    {
                        throw new NotSupportedException($"{attributedType.FullName} is not an expected attributed type in dependency auto registration.");
                    }
                    
                    if (attribute == typeof(Transient))
                    {
                        services.AddTransient(attributedType, implementedType);
                    }
                    else if (attribute == typeof(Scoped))
                    {
                        services.AddScoped(attributedType, implementedType);
                    }
                    else if (attribute == typeof(Singleton))
                    {
                        services.AddSingleton(attributedType, implementedType);
                    }
                    else
                    {
                        throw new NotSupportedException($"{attribute.FullName} is not supported in dependency auto registration");
                    }

                    registrations[attributedType] = implementedType;
                }
            }
            
            return registrations;
        }
    }
}
