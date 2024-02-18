using FluentValidation;

namespace VendingMachine.Application.Transaction.Queries
{
    public class GetTransactionsByUserIdQueryValidator : AbstractValidator<GetTransactionsByUserIdQuery>
    {
        public GetTransactionsByUserIdQueryValidator()
        {
            RuleFor(t => t.UserId).GreaterThan(0);
        }
    }
}
