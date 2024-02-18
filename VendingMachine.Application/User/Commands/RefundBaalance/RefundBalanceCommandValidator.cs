using FluentValidation;

namespace VendingMachine.Application.User.Commands.RefundBaalance
{
    public class RefundBalanceCommandValidator : AbstractValidator<RefundBalanceCommand>
    {
        public RefundBalanceCommandValidator()
        {
            RuleFor(p => p.UserId).GreaterThan(0);
        }
    }
}
