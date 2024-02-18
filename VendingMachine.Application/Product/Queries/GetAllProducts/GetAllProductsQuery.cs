using MediatR;
using VendingMachine.Application.Product.Dtos;

namespace VendingMachine.Application.Product.Queries.GetAllProducts
{
    public class GetAllProductsQuery : IRequest<List<ProductDto>>
    {
    }
}
