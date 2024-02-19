using Microsoft.Extensions.Logging;
using Moq;
using VendingMachine.Application.Common.Interfaces;
using VendingMachine.Application.User.Commands.RefundBaalance;

namespace VendingMachine.Application.UnitTests.User.Commands.RefundBaalance
{
    public class RefundBalanceCommandHandlerTests
    {
        [Fact]
        public async Task GivenValidUserId_WhenHandling_ThenReturnsUserBalanceRefundDto()
        {
            // Arrange
            var userId = 1;
            var cancellationToken = new CancellationToken();
            var request = new RefundBalanceCommand { UserId = userId };

            var user = new Entities.User { UserId = userId, Balance = 100 };

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(userId, cancellationToken))
                              .ReturnsAsync(user);
            userRepositoryMock.Setup(repo => repo.UpdateUserAsync(It.IsAny<Entities.User>(), cancellationToken))
                              .ReturnsAsync((Entities.User userRequest, CancellationToken ct) =>
                              {
                                  user.Balance = userRequest.Balance;
                                  return user;
                              });

            var loggerMock = new Mock<ILogger<RefundBalanceCommandHandler>>();

            var handler = new RefundBalanceCommandHandler(
                userRepositoryMock.Object,
                loggerMock.Object);

            // Act
            var result = await handler.Handle(request, cancellationToken);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(100, result.ChangeAmount);
        }

        [Fact]
        public async Task GivenInvalidUserId_WhenHandling_ThenThrowsException()
        {
            // Arrange
            var userId = 1;
            var cancellationToken = new CancellationToken();
            var request = new RefundBalanceCommand { UserId = userId };

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(userId, cancellationToken))
                              .ReturnsAsync((Entities.User)null);

            var loggerMock = new Mock<ILogger<RefundBalanceCommandHandler>>();

            var handler = new RefundBalanceCommandHandler(
                userRepositoryMock.Object,
                loggerMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => handler.Handle(request, cancellationToken));
        }
    }
}
