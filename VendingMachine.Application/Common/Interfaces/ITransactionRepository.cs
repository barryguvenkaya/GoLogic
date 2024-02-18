namespace VendingMachine.Application.Common.Interfaces
{
    public interface ITransactionRepository
    {
        public Task<List<Entities.Transaction>> GetUserTransactionsAsync(int userId, CancellationToken cancellationToken);
        public Task CreateTransactionAsync(Entities.Transaction transaction, CancellationToken cancellationToken);
    }
}
