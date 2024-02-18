using FluentValidation;

namespace VendingMachine.Application.Product.Queries.GetProductById
{
    public class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
    {
        public GetProductByIdQueryValidator()
        {
            RuleFor(p => p.ProductId).GreaterThan(0);
        }
    }
}
