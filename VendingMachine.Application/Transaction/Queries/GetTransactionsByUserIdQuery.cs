using MediatR;
using VendingMachine.Application.Transaction.Dtos;

namespace VendingMachine.Application.Transaction.Queries
{
    public class GetTransactionsByUserIdQuery(int userId) : IRequest<List<TransactionDto>>
    {
        public int UserId { get; set; } = userId;
    }
}
