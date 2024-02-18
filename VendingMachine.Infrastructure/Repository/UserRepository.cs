using Microsoft.EntityFrameworkCore;
using VendingMachine.Application.Common.Interfaces;
using VendingMachine.Application.Entities;
using VendingMachine.Infrastructure.Persistence;

namespace VendingMachine.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        public UserRepository()
        {
            using var context = new VendingMachineDbContext();
            var Users = new List<User>
            {
                new() { UserId = 1, Balance = 20},
                new() { UserId = 2, Balance = 15},
                new() { UserId = 3, Balance = 0},
            };
            context.Users.AddRange(Users);
            context.SaveChanges();
        }

        public async Task<List<User>> GetUsersAsync(CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(cancellationToken);

            using var context = new VendingMachineDbContext();
            return [.. await context.Users.ToListAsync(cancellationToken)];
        }

        public async Task<User> GetUserByIdAsync(int userId, CancellationToken cancellationToken)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(userId, 1);
            ArgumentNullException.ThrowIfNull(cancellationToken);

            using var context = new VendingMachineDbContext();
            return await context.Users.FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken: cancellationToken);
        }

        public async Task<User> UpdateUserAsync(User userRequest, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(userRequest);
            ArgumentNullException.ThrowIfNull(cancellationToken);

            using var context = new VendingMachineDbContext();
            var user = await context.Users.FirstOrDefaultAsync(x => x.UserId == userRequest.UserId, cancellationToken: cancellationToken) 
                ?? throw new InvalidOperationException($"User {userRequest.UserId} not found");

            user.Balance = userRequest.Balance;

            await context.SaveChangesAsync(cancellationToken);

            return user;
        }
    }
}
