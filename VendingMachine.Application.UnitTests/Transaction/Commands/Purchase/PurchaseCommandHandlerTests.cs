using Microsoft.Extensions.Logging;
using Moq;
using VendingMachine.Application.Common.Interfaces;
using VendingMachine.Application.Transaction.Commands.Purchase;

namespace VendingMachine.Application.UnitTests.Transaction.Commands.Purchase
{
    public class PurchaseCommandHandlerTests
    {
        [Fact]
        public async Task GivenValidRequest_WhenHandling_ThenReturnsPurchaseResultDto()
        {
            // Arrange
            var cancellationToken = new CancellationToken();
            var request = new PurchaseCommand { UserId = 1, ProductId = 1, Quantity = 1 };

            var product = new Entities.Product { ProductId = 1, Name = "Product A", Price = 10, Stock = 5 };
            var user = new Entities.User { UserId = 1, Balance = 50 };

            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(repo => repo.GetProductsAsync(cancellationToken))
                                 .ReturnsAsync(new List<Entities.Product> { product });
            productRepositoryMock.Setup(repo => repo.SubtractQuantityAsync(request.ProductId, request.Quantity, cancellationToken))
                                 .Returns(Task.CompletedTask);

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(request.UserId, cancellationToken))
                              .ReturnsAsync(user);
            userRepositoryMock.Setup(repo => repo.UpdateUserAsync(It.IsAny<Entities.User>(), cancellationToken))
                              .ReturnsAsync((Entities.User userRequest, CancellationToken cancellationToken) =>
                              {
                                  user.Balance = userRequest.Balance;
                                  return user;
                              });

            var transactionRepositoryMock = new Mock<ITransactionRepository>();
            transactionRepositoryMock.Setup(repo => repo.CreateTransactionAsync(It.IsAny<Entities.Transaction>(), cancellationToken))
                                     .Returns(Task.CompletedTask);

            var loggerMock = new Mock<ILogger<PurchaseCommandHandler>>();

            var handler = new PurchaseCommandHandler(
                productRepositoryMock.Object,
                userRepositoryMock.Object,
                transactionRepositoryMock.Object,
                loggerMock.Object);

            // Act
            var result = await handler.Handle(request, cancellationToken);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(40, result.BalanceRemaining);
            Assert.Equal("Product A", result.ProductName);
            Assert.Equal(1, result.Quantity);
        }

        [Fact]
        public async Task GivenInvalidProductId_WhenHandling_ThenThrowsInvalidOperationException()
        {
            // Arrange
            var cancellationToken = new CancellationToken();
            var request = new PurchaseCommand { UserId = 1, ProductId = 100, Quantity = 1 };

            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(repo => repo.GetProductsAsync(cancellationToken))
                                 .ReturnsAsync(new List<Entities.Product>());

            var userRepositoryMock = new Mock<IUserRepository>();
            var transactionRepositoryMock = new Mock<ITransactionRepository>();
            var loggerMock = new Mock<ILogger<PurchaseCommandHandler>>();

            var handler = new PurchaseCommandHandler(
                productRepositoryMock.Object,
                userRepositoryMock.Object,
                transactionRepositoryMock.Object,
                loggerMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => handler.Handle(request, cancellationToken));
        }
    }
}
