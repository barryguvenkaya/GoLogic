using FluentValidation;

namespace VendingMachine.Application.Transaction.Commands.Purchase
{
    public class PurchaseCommandValidator : AbstractValidator<PurchaseCommand>
    {
        public PurchaseCommandValidator()
        {
            RuleFor(p => p.ProductId).GreaterThan(0);
            RuleFor(p => p.UserId).GreaterThan(0);
            RuleFor(p => p.Quantity).GreaterThan(0);
        }
    }
}
