using ProductTracker.Application.Interfaces;
using ProductTracker.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;
using ProductTracker.Infrastructure.Context;

namespace ProductTracker.Infrastructure
{
    public static class ServiceCollectionExtension
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IOrganizationRepository, OrganizationRepository>();

            services.AddTransient<IBatchDataRepository, BatchDataRepository>();
            services.AddTransient<ICouponsDataRepository, CouponsDataRepository>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddSingleton<DapperContext>();
        }
    }
}
