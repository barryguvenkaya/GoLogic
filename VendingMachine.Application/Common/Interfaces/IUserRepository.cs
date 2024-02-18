namespace VendingMachine.Application.Common.Interfaces
{
    public interface IUserRepository
    {
        public Task<List<Entities.User>> GetUsersAsync(CancellationToken cancellationToken);
        public Task<Entities.User> GetUserByIdAsync(int userId, CancellationToken cancellationToken);
        public Task<Entities.User> UpdateUserAsync(Entities.User newUser, CancellationToken cancellationToken);
    }
}
