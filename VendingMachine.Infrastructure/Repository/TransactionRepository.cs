using Microsoft.EntityFrameworkCore;
using VendingMachine.Application.Common.Interfaces;
using VendingMachine.Application.Entities;
using VendingMachine.Infrastructure.Persistence;

namespace VendingMachine.Infrastructure.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        public async Task<List<Transaction>> GetUserTransactionsAsync(int userId, CancellationToken cancellationToken)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(userId, 1);
            ArgumentNullException.ThrowIfNull(cancellationToken);

            using var context = new VendingMachineDbContext();
            return [.. await context.Transactions.Where(w => w.UserId == userId).ToListAsync(cancellationToken)];
        }

        public async Task CreateTransactionAsync(Transaction transaction, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(transaction);
            ArgumentNullException.ThrowIfNull(cancellationToken);

            using var context = new VendingMachineDbContext();

            var maxTransactionId = await context.Transactions.MaxAsync(m => (int?)m.TransactionId) ?? 0;

            var newTransactionId = maxTransactionId + 1;
            transaction.TransactionId = newTransactionId;

            await context.Transactions.AddAsync(transaction);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
