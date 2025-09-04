using Repository;
using Services;
using System.Reflection;
using Utils;

namespace WebApi.Extensions
{
    public static class ServiceRegistration
    {
        public static void RegisterServices(this IServiceCollection services, params Assembly[] assemblies)
        {
            var types = assemblies.SelectMany(a => a.GetTypes());

            // Services 
            var serviceTypes = types.Where(t => typeof(IBaseService).IsAssignableFrom(t) && t.IsClass);
            foreach (var serviceType in serviceTypes)
            {
                var iface = serviceType.GetInterfaces().FirstOrDefault(i => i != typeof(IBaseService));
                if (iface != null)
                    services.AddScoped(iface, serviceType);
            }

            // Utils
            var utilTypes = types.Where(t => typeof(IBaseUtil).IsAssignableFrom(t) && t.IsClass);
            foreach (var utilType in utilTypes)
            {
                var iface = utilType.GetInterfaces().FirstOrDefault(i => i != typeof(IBaseUtil));
                if (iface != null)
                    services.AddScoped(iface, utilType);
            }
        }
    }

}
