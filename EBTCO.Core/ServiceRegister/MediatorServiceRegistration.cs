using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EBTCO.Core.ServiceRegister
{
    public static class MediatorServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            return services;
        }
    }
}