using MediatR;
using VendingMachine.Application.Transaction.Dtos;

namespace VendingMachine.Application.Transaction.Queries.GetTransactionsByUserId
{
    public class GetTransactionsByUserIdQuery(int userId) : IRequest<List<ReceiptDto>>
    {
        public int UserId { get; set; } = userId;
    }
}
