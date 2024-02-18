using MediatR;
using VendingMachine.Application.Product.Dtos;

namespace VendingMachine.Application.Product.Queries.GetProductById
{
    public class GetProductByIdQuery : IRequest<ProductDto>
    {
        public GetProductByIdQuery(int productId)
        {
            ProductId = productId;
        }

        public int ProductId { get; set; }
    }
}
