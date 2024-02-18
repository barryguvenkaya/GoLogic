using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VendingMachine.Application.Common.Interfaces;
using VendingMachine.Application.Product.Dtos;

namespace VendingMachine.Application.Product.Queries.GetAllProducts
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<ProductDto>>
    {
        private readonly IProductRepository _productContext;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllProductsQueryHandler> _logger;
        public GetAllProductsQueryHandler(
            IProductRepository productContext,
            IMapper mapper,
            ILogger<GetAllProductsQueryHandler> logger)
        {
            _productContext = productContext ?? throw new ArgumentNullException(nameof(productContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<List<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);
            ArgumentNullException.ThrowIfNull(cancellationToken);

            _logger.LogInformation("Handling GetAllProductsQuery");

            var products = await _productContext.GetProductsAsync(cancellationToken);
            return _mapper.Map<List<ProductDto>>(products);
        }
    }
}
