using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using VendingMachine.Application.Common.Interfaces;
using VendingMachine.Application.Product.Dtos;
using VendingMachine.Application.Product.Queries.GetAllProducts;

namespace VendingMachine.Application.UnitTests.Product.Queries.GetAllProducts
{
    public class GetAllProductsQueryHandlerTests
    {
        [Fact]
        public async Task GivenValidQuery_WhenHandling_ThenReturnsListOfProductDto()
        {
            // Arrange
            var cancellationToken = new CancellationToken();
            var products = new List<Entities.Product>();
            var productDtos = new List<ProductDto>();

            // Mock dependencies
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(repo => repo.GetProductsAsync(cancellationToken))
                                 .ReturnsAsync(products);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<List<ProductDto>>(It.IsAny<List<Entities.Product>>()))
                      .Returns(productDtos);

            var loggerMock = new Mock<ILogger<GetAllProductsQueryHandler>>();

            var handler = new GetAllProductsQueryHandler(
                productRepositoryMock.Object,
                mapperMock.Object,
                loggerMock.Object);

            var query = new GetAllProductsQuery();

            // Act
            var result = await handler.Handle(query, cancellationToken);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<ProductDto>>(result);
            Assert.Equal(productDtos, result);
        }
    }
}
