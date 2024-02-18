using MediatR;
using Microsoft.Extensions.Logging;
using VendingMachine.Application.Common.Interfaces;
using VendingMachine.Application.Transaction.Dtos;

namespace VendingMachine.Application.Transaction.Commands.Purchase
{
    public class PurchaseCommandHandler : IRequestHandler<PurchaseCommand, PurchaseResultDto>
    {
        private readonly IProductRepository _productContext;
        private readonly IUserRepository _userContext;
        private readonly ITransactionRepository _transactionContext;
        private readonly ILogger<PurchaseCommandHandler> _logger;

        public PurchaseCommandHandler(
            IProductRepository productContext,
            IUserRepository userContext,
            ITransactionRepository transactionContext,
            ILogger<PurchaseCommandHandler> logger
            )
        {
            _productContext = productContext ?? throw new ArgumentNullException(nameof(productContext));
            _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
            _transactionContext = transactionContext ?? throw new ArgumentNullException(nameof(transactionContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<PurchaseResultDto> Handle(PurchaseCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);
            ArgumentNullException.ThrowIfNull(cancellationToken);

            _logger.LogInformation("Handling PurchaseCommand for user id {UserId}. Requested product {ProductId}, quantity {Quantity}", request.UserId, request.ProductId, request.Quantity);

            var products = await _productContext.GetProductsAsync(cancellationToken);
            var product = products.FirstOrDefault(x => x.ProductId == request.ProductId);
            
            if (product == null)
            {
                throw new InvalidOperationException($"Product {request.ProductId} cannot be found.");
            }

            if (product.Stock < request.Quantity)
            {
                throw new InvalidOperationException($"Not enough stock for Product {request.ProductId}. Remaining stock is {product.Stock}");
            }

            var user = await _userContext.GetUserByIdAsync(request.UserId, cancellationToken);

            var totalAmount = request.Quantity * product.Price;
            var newBalance = user.Balance - totalAmount;

            if (newBalance < 0)
            {
                throw new InvalidOperationException($"Not enough user balance. User balance for {request.UserId} is {user.Balance}");
            }

            await _productContext.SubtractQuantityAsync(request.ProductId, request.Quantity, cancellationToken);

            await _userContext.UpdateUserAsync(new Entities.User { UserId = request.UserId, Balance = newBalance }, cancellationToken);

            var transaction = new Entities.Transaction
            {
                ProductId = request.ProductId,
                Quantity = request.Quantity,
                UserId = request.UserId,
                TimestampUtc = DateTime.UtcNow };

            await _transactionContext.CreateTransactionAsync(transaction, cancellationToken);

            var result = new PurchaseResultDto
            {
                BalanceRemaining = newBalance,
                ProductName = product.Name,
                Quantity = request.Quantity
            };

            return result;
        }
    }
}
