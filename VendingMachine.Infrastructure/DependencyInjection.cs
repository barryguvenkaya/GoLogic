using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VendingMachine.Application.Common.Interfaces;
using VendingMachine.Infrastructure.Persistence;
using VendingMachine.Infrastructure.Repository;

namespace VendingMachine.Infrastructure
{
    public static class DependencyInjection
    {
        /// <summary>
        /// For external systems such as db context, EF configs, db services, http services.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Health checks and AddDbContext can be added here for the actual database (instead of in-memory database)

            services.AddScoped<DbContext>(provider => provider.GetService<VendingMachineDbContext>());
            services.AddSingleton<IProductRepository, ProductRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<ITransactionRepository, TransactionRepository>();
            return services;
        }
    }
}
