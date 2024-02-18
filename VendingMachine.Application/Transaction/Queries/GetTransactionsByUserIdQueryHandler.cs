using MediatR;
using Microsoft.Extensions.Logging;
using VendingMachine.Application.Common.Interfaces;
using VendingMachine.Application.Common.Services;
using VendingMachine.Application.Transaction.Dtos;

namespace VendingMachine.Application.Transaction.Queries
{
    public class GetTransactionsByUserIdQueryHandler : IRequestHandler<GetTransactionsByUserIdQuery, List<TransactionDto>>
    {
        private readonly ITransactionRepository _transactionContext;
        private readonly IProductRepository _productContext;
        private readonly ILogger<GetTransactionsByUserIdQueryHandler> _logger;
        public GetTransactionsByUserIdQueryHandler(
            ITransactionRepository transactionContext,
            IProductRepository productContext,
            ILogger<GetTransactionsByUserIdQueryHandler> logger
            )
        {
            _transactionContext = transactionContext ?? throw new ArgumentNullException(nameof(transactionContext));
            _productContext = productContext ?? throw new ArgumentNullException(nameof(productContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<List<TransactionDto>> Handle(GetTransactionsByUserIdQuery request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);
            ArgumentNullException.ThrowIfNull(cancellationToken);

            _logger.LogInformation("Handling GetTransactionsByUserIdQuery for user id {UserId}.", request.UserId);

            var result = new List<TransactionDto>();

            var transactions = await _transactionContext.GetUserTransactionsAsync(request.UserId, cancellationToken);

            if (transactions == null)
            {
                return result;
            }

            var products = await _productContext.GetProductsAsync(cancellationToken);

            if (products == null)
            {
                throw new Exception("No products exists");
            }

            var transactionsDto = from product in products
                             join transaction in transactions
                             on product.ProductId equals transaction.ProductId
                             select new
                             {
                                 ProductName = product.Name,
                                 Quantity = transaction.Quantity,
                                 Price = product.Price,
                                 Total = transaction.Quantity * product.Price,
                                 TimestampLocal = DateTimeProvider.ConvertUtcToLocal(transaction.TimestampUtc)
                             };

            foreach (var transactionDto in transactionsDto)
            {
                result.Add(new TransactionDto 
                { 
                    Price = transactionDto.Price,
                    ProductName = transactionDto.ProductName,
                    Quantity = transactionDto.Quantity,
                    TimestampLocal = transactionDto.TimestampLocal,
                    TotalPaid = transactionDto.Total
                });
            }

            return result;
        }
    }
}
