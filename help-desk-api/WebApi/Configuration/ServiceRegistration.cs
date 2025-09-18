using System.Reflection;
using Microsoft.Extensions.DependencyInjection;


namespace WebApi.Configuration
{
    public static class ServiceRegistration
    {
        public static void AddAllServices(this IServiceCollection services, Assembly assembly)
        {
            // Manually register all classes ending with "Service" as their implemented interfaces with scoped lifetime
            var serviceTypes = assembly.GetTypes()
                .Where(type => type.IsClass && !type.IsAbstract && type.Name.EndsWith("Service"));

            foreach (var implementationType in serviceTypes)
            {
                var interfaces = implementationType.GetInterfaces();
                foreach (var serviceType in interfaces)
                {
                    services.AddScoped(serviceType, implementationType);
                }
            }
        }
        public static void AddAllRepositories(this IServiceCollection services, Assembly assembly)
        {
            // Manually register all classes ending with "Repository" as their implemented interfaces with scoped lifetime

            var repositoryTypes = assembly.GetTypes()
                .Where(type => type.IsClass && !type.IsAbstract && type.Name.EndsWith("Repository"));

            foreach (var implementationType in repositoryTypes)
            {
                var interfaces = implementationType.GetInterfaces();
                foreach (var serviceType in interfaces)
                {
                    services.AddScoped(serviceType, implementationType);
                }
            }
        }
    }

}
