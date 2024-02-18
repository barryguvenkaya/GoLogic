using Microsoft.EntityFrameworkCore;
using VendingMachine.Application.Common.Interfaces;
using VendingMachine.Application.Entities;
using VendingMachine.Infrastructure.Persistence;

namespace VendingMachine.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        public ProductRepository() 
        {
            using var context = new VendingMachineDbContext();
            var products = new List<Product>
            {
                new() { ProductId = 1, Name = "Water", Price = 2, Stock = 3 },
                new() { ProductId = 2, Name = "Coke", Price = 5, Stock = 0 },
                new() { ProductId = 3, Name = "Chips", Price = 7, Stock = 5 },
                new() { ProductId = 4, Name = "Chocolate", Price = 10, Stock = 6}
            };
            context.Products.AddRange(products);
            context.SaveChanges();
        }

        public async Task<List<Product>> GetProductsAsync(CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(cancellationToken);

            using var context = new VendingMachineDbContext();
            return [.. await context.Products.ToListAsync(cancellationToken)];
        }

        public async Task SubtractQuantityAsync(int productId, int subtractQuantity, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(cancellationToken);

            using var context = new VendingMachineDbContext();
            var product = await context.Products.FirstOrDefaultAsync(x => x.ProductId == productId);

            if (product == null)
            {
                throw new InvalidOperationException($"Product {productId} cannot be found.");
            }

            var initialQuantity = product.Stock;

            product.Stock -= subtractQuantity;

            if (product.Stock < 0)
            {
                throw new InvalidOperationException($"Not enough stock. Remaining stock quantity is {initialQuantity}.");
            }

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
