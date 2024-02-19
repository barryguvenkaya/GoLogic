using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using VendingMachine.Application.Common.Interfaces;
using VendingMachine.Application.User.Commands.DepositBalance;
using VendingMachine.Application.User.Dtos;

namespace VendingMachine.Application.UnitTests.User.Commands.DepositBalance
{
    public class DepositBalanceCommandHandlerTests
    {
        [Fact]
        public async Task GivenValidRequest_WhenHandling_ThenReturnsUserBalanceDto()
        {
            // Arrange
            var userId = 1;
            var depositAmount = 50;
            var cancellationToken = new CancellationToken();
            var request = new DepositBalanceCommand { UserId = userId, DepositAmount = depositAmount };

            var user = new Entities.User { UserId = userId, Balance = 100 };

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(userId, cancellationToken))
                              .ReturnsAsync(user);
            userRepositoryMock.Setup(repo => repo.UpdateUserAsync(It.IsAny<Entities.User>(), cancellationToken))
                              .ReturnsAsync((Entities.User updatedUser, CancellationToken ct) =>
                              {
                                  user.Balance = updatedUser.Balance;
                                  return user;
                              });

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<UserBalanceDto>(user))
                      .Returns(new UserBalanceDto { UserId = userId, Balance = user.Balance + depositAmount });

            var loggerMock = new Mock<ILogger<DepositBalanceCommandHandler>>();

            var handler = new DepositBalanceCommandHandler(
                userRepositoryMock.Object,
                mapperMock.Object,
                loggerMock.Object);

            // Act
            var result = await handler.Handle(request, cancellationToken);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userId, result.UserId);
            Assert.Equal(150, result.Balance);
        }

        [Fact]
        public async Task GivenInvalidUserId_WhenHandling_ThenThrowsException()
        {
            // Arrange
            var userId = 1;
            var depositAmount = 50;
            var cancellationToken = new CancellationToken();
            var request = new DepositBalanceCommand { UserId = userId, DepositAmount = depositAmount };

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(userId, cancellationToken))
                              .ReturnsAsync((Entities.User)null);

            var mapperMock = new Mock<IMapper>();

            var loggerMock = new Mock<ILogger<DepositBalanceCommandHandler>>();

            var handler = new DepositBalanceCommandHandler(
                userRepositoryMock.Object,
                mapperMock.Object,
                loggerMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => handler.Handle(request, cancellationToken));
        }
    }
}
