using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VendingMachine.Application.Behaviours;
using VendingMachine.Application.Common.Automapper;

namespace VendingMachine.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, Assembly apiAssembly, IConfiguration configuration)
        {
            var assemblies = new Assembly[] { apiAssembly, Assembly.GetExecutingAssembly() };

            services.AddAutoMapper(assemblies, ServiceLifetime.Transient);
            MappingProfile.ConsumingApplicationAssemblies = assemblies;

            foreach (var assembly in assemblies)
            {
                services.AddFluentValidation(assembly);
            }

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));
            services.AddMediatR(cfg => ConfigureMediatR(cfg, assemblies));

            return services;
        }

        private static void ConfigureMediatR(MediatRServiceConfiguration cfg, Assembly[] assemblies)
        {
            cfg.RegisterServicesFromAssemblies(assemblies);
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehaviour<,>));
            cfg.Lifetime = ServiceLifetime.Scoped;
        }

        private static void AddFluentValidation(this IServiceCollection services, Assembly assembly)
        {
            var validatorType = typeof(IValidator<>);

            var validatorTypes = assembly
                .GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType &&
                    i.GetGenericTypeDefinition() == validatorType))
                .ToList();

            foreach (var validator in validatorTypes)
            {
                var requestType = validator.GetInterfaces()
                    .Where(i => i.IsGenericType &&
                        i.GetGenericTypeDefinition() == typeof(IValidator<>))
                    .Select(i => i.GetGenericArguments()[0])
                    .First();

                var validatorInterface = validatorType
                    .MakeGenericType(requestType);

                services.AddTransient(validatorInterface, validator);
            }
        }
    }
}
