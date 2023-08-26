using ProductTracker.Application.Interfaces;

namespace ProductTracker.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IUserRepository userRepository, IProductRepository products,
            IOrganizationRepository organizations,IBatchDataRepository batchDataRepository,
            ICouponsDataRepository couponsDataRepository, IPlantRepository plants)
        {
            Users = userRepository;
            Products = products;
            Organizations = organizations;
            Batches = batchDataRepository;
            Coupons = couponsDataRepository;
            Plants = plants;
        }

        public IUserRepository Users { get; set; }

        public IProductRepository Products { get; set; }
        public IOrganizationRepository Organizations { get; set; }
        public IBatchDataRepository Batches { get; set; }
        public ICouponsDataRepository Coupons { get; set; }

        public IPlantRepository Plants  { get; set; }
}
}
