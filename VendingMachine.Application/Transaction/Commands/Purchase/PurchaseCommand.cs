using MediatR;
using VendingMachine.Application.Transaction.Dtos;

namespace VendingMachine.Application.Transaction.Commands.Purchase
{
    public class PurchaseCommand : IRequest<PurchaseResultDto>
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
