using ProductTracker.Application.Interfaces;
using ProductTracker.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;
using ProductTracker.Infrastructure.Context;
using ProductTracker.Application.Interfaces.FileStorage;
using ProductTracker.Infrastructure.Repository.FileStorage;

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
            services.AddTransient<IPlantRepository, PlantRepository>();

            services.AddTransient<IProductTypeRepository, ProductType>();

            services.AddTransient<IProductWeightRepository, ProductWeight>();
            services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();

            services.AddTransient<ITemplateRepository, TemplateRepository>();


            services.AddSingleton<IFileStorageProvider, AzureBlobStorageProvider>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            

            // JwtUtils : IJwtUtils

            services.AddSingleton<DapperContext>();
        }
    }
}
