namespace VendingMachine.Application.Common.Interfaces
{
    public interface IProductRepository
    {
        public Task<List<Entities.Product>> GetProductsAsync(CancellationToken cancellationToken);
        public Task SubtractQuantityAsync(int productId, int subtractQuantity, CancellationToken cancellationToken);

    }
}
