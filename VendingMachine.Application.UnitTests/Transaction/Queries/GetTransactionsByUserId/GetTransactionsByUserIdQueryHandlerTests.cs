using Microsoft.Extensions.Logging;
using Moq;
using VendingMachine.Application.Common.Interfaces;
using VendingMachine.Application.Transaction.Queries.GetTransactionsByUserId;

namespace VendingMachine.Application.UnitTests.Transaction.Queries.GetTransactionsByUserId
{
    public class GetTransactionsByUserIdQueryHandlerTests
    {
        [Fact]
        public async Task GivenValidUserId_WhenHandling_ThenReturnsReceiptDtos()
        {
            // Arrange
            var userId = 1;
            var cancellationToken = new CancellationToken();
            var request = new GetTransactionsByUserIdQuery(userId);

            var transactions = new List<Entities.Transaction> 
            {
                new Entities.Transaction { TransactionId = 1, UserId = userId, ProductId = 1, Quantity = 2, TimestampUtc = DateTime.UtcNow }
            };

            var products = new List<Entities.Product> 
            {
                new Entities.Product { ProductId = 1, Name = "Product A", Price = 10 }
            };

            var transactionRepositoryMock = new Mock<ITransactionRepository>();
            transactionRepositoryMock.Setup(repo => repo.GetUserTransactionsAsync(userId, cancellationToken))
                                     .ReturnsAsync(transactions);

            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(repo => repo.GetProductsAsync(cancellationToken))
                                 .ReturnsAsync(products);

            var loggerMock = new Mock<ILogger<GetTransactionsByUserIdQueryHandler>>();

            var handler = new GetTransactionsByUserIdQueryHandler(
                transactionRepositoryMock.Object,
                productRepositoryMock.Object,
                loggerMock.Object);

            // Act
            var result = await handler.Handle(request, cancellationToken);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            var receiptDto = result.First();
            Assert.Equal(20, receiptDto.TotalPaid);
        }

        [Fact]
        public async Task GivenNoTransactions_WhenHandling_ThenReturnsEmptyList()
        {
            // Arrange
            var userId = 1;
            var cancellationToken = new CancellationToken();
            var request = new GetTransactionsByUserIdQuery(userId);

            var transactionRepositoryMock = new Mock<ITransactionRepository>();
            transactionRepositoryMock.Setup(repo => repo.GetUserTransactionsAsync(userId, cancellationToken))
                                     .ReturnsAsync((List<Entities.Transaction>)null); // Simulating no transactions returned

            var productRepositoryMock = new Mock<IProductRepository>();

            var loggerMock = new Mock<ILogger<GetTransactionsByUserIdQueryHandler>>();

            var handler = new GetTransactionsByUserIdQueryHandler(
                transactionRepositoryMock.Object,
                productRepositoryMock.Object,
                loggerMock.Object);

            // Act
            var result = await handler.Handle(request, cancellationToken);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}
