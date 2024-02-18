using MediatR;
using VendingMachine.Application.User.Dtos;

namespace VendingMachine.Application.User.Commands.DepositBalance
{
    public class DepositBalanceCommand : IRequest<UserBalanceDto>
    {
        public int UserId { get; set; }
        public decimal DepositAmount { get; set; }
    }
}
