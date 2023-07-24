namespace ProductTracker.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IContactRepository Contacts { get; }

        IProductRepository Products { get; }

        IOrganizationRepository Organizations { get; }

        IBatchDataRepository Batches { get; }
        ICouponsDataRepository Coupons { get; }

    }
}
