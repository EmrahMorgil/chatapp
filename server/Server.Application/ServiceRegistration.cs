using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Server.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationRegistration(this IServiceCollection services)
        {
            var assm = Assembly.GetExecutingAssembly();
            services.AddAutoMapper(assm);
            services.AddMediatR(assm);
        }
    }
}
