using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VendingMachine.Application.Common.Interfaces;
using VendingMachine.Application.Product.Dtos;

namespace VendingMachine.Application.Product.Queries.GetProductById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly IProductRepository _productContext;
        private readonly IMapper _mapper;
        private readonly ILogger<GetProductByIdQueryHandler> _logger;
        public GetProductByIdQueryHandler(
            IProductRepository productContext,
            IMapper mapper,
            ILogger<GetProductByIdQueryHandler> logger)
        {
            _productContext = productContext ?? throw new ArgumentNullException(nameof(productContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);
            ArgumentNullException.ThrowIfNull(cancellationToken);

            _logger.LogInformation("Handling GetProductByIdQuery for product id {ProductId}", request.ProductId);

            var products = await _productContext.GetProductsAsync(cancellationToken);
            var product = products.FirstOrDefault(p => p.ProductId == request.ProductId) ?? throw new InvalidOperationException($"Product with id {request.ProductId} not found");
            var productDto = _mapper.Map<ProductDto>(product);

            return productDto;
        }
    }
}
