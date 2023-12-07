using ProductTracker.Application.Interfaces;

namespace ProductTracker.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IUserRepository userRepository, IProductRepository products,
            IOrganizationRepository organizations,IBatchDataRepository batchDataRepository,
            ICouponsDataRepository couponsDataRepository, IPlantRepository plants, IProductTypeRepository producttypes, IProductWeightRepository productWeights, IProductCategoryRepository productCategorys, ITemplateRepository templateRepositorys)
        {
            Users = userRepository;
            Products = products;
            Organizations = organizations;
            Batches = batchDataRepository;
            Coupons = couponsDataRepository;
            Plants = plants;
            ProductWeights = productWeights;
            ProductTypes = producttypes;
            ProductCategorys = productCategorys;
            TemplateRepositorys = templateRepositorys;
        }

        public IUserRepository Users { get; set; }

        public IProductRepository Products { get; set; }
        public IOrganizationRepository Organizations { get; set; }
        public IBatchDataRepository Batches { get; set; }
        public ICouponsDataRepository Coupons { get; set; }

        public IPlantRepository Plants  { get; set; }

        public ITemplateRepository TemplateRepositorys { get; set; }

        public IProductTypeRepository ProductTypes  { get; set; }

    public IProductWeightRepository ProductWeights { get; set; }
        public IProductCategoryRepository ProductCategorys { get; set; }


    }
}
