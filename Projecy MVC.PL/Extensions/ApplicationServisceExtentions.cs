using Microsoft.Extensions.DependencyInjection;
using ProjectMVC.BLL.Interfacies;
using ProjectMVC.BLL.Repositories;

namespace Projecy_MVC.PL.Extensions
{
    public static class ApplicationServisceExtentions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection  services)
        {
            //services.AddScoped<IDepartementRepository, DepartementRepository>();
            //services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
