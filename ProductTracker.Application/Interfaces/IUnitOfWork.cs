﻿namespace ProductTracker.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }

        IProductRepository Products { get; }

        IOrganizationRepository Organizations { get; }

        IBatchDataRepository Batches { get; }
        ICouponsDataRepository Coupons { get; }
        IPlantRepository Plants { get; }

        IProductTypeRepository ProductTypes { get; }
        IProductWeightRepository ProductWeights { get; }
        IProductCategoryRepository ProductCategorys { get; }
        ITemplateRepository TemplateRepositorys { get; }

    }
}
