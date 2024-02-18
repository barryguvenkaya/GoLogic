using MediatR;
using VendingMachine.Application.User.Dtos;

namespace VendingMachine.Application.User.Commands.RefundBaalance
{
    public class RefundBalanceCommand : IRequest<UserBalanceRefundDto>
    {
        public int UserId { get; set; }
    }
}
