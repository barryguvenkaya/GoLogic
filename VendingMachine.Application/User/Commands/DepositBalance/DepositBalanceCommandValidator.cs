using FluentValidation;

namespace VendingMachine.Application.User.Commands.DepositBalance
{
    public class DepositBalanceCommandValidator : AbstractValidator<DepositBalanceCommand>
    {
        public DepositBalanceCommandValidator()
        {
            RuleFor(p => p.UserId).GreaterThan(0);
            RuleFor(p => p.DepositAmount).GreaterThan(0);
        }
    }
}
