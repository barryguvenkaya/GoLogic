using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using VendingMachine.Application.Common.Interfaces;
using VendingMachine.Application.Product.Dtos;
using VendingMachine.Application.Product.Queries.GetProductById;

namespace VendingMachine.Application.UnitTests.Product.Queries.GetProductById
{
    public class GetProductByIdQueryHandlerTests
    {
        [Fact]
        public async Task GivenValidProductId_WhenHandling_ThenReturnsProductDto()
        {
            // Arrange
            int productId = 1;
            var cancellationToken = new CancellationToken();
            var product = new Entities.Product { ProductId = productId };
            var productDto = new ProductDto();

            // Mock dependencies
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(repo => repo.GetProductsAsync(cancellationToken))
                                 .ReturnsAsync(new List<Entities.Product> { product });

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<ProductDto>(product))
                      .Returns(productDto);

            var loggerMock = new Mock<ILogger<GetProductByIdQueryHandler>>();

            var handler = new GetProductByIdQueryHandler(
                productRepositoryMock.Object,
                mapperMock.Object,
                loggerMock.Object);

            var query = new GetProductByIdQuery(productId);

            // Act
            var result = await handler.Handle(query, cancellationToken);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ProductDto>(result);
            Assert.Equal(productDto, result);
        }

        [Fact]
        public async Task GivenInvalidProductId_WhenHandling_ThenThrowsInvalidOperationException()
        {
            // Arrange
            int invalidProductId = -1; 
            var cancellationToken = new CancellationToken();

            // Mock dependencies
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(repo => repo.GetProductsAsync(cancellationToken))
                                 .ReturnsAsync(new List<Entities.Product>());

            var mapperMock = new Mock<IMapper>();

            var loggerMock = new Mock<ILogger<GetProductByIdQueryHandler>>();

            var handler = new GetProductByIdQueryHandler(
                productRepositoryMock.Object,
                mapperMock.Object,
                loggerMock.Object);

            var query = new GetProductByIdQuery(invalidProductId);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => handler.Handle(query, cancellationToken));
        }
    }
}
