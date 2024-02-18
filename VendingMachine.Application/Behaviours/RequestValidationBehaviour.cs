using FluentValidation;
using MediatR;

namespace VendingMachine.Application.Behaviours
{
    public class RequestValidationBehaviour<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IBaseRequest
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var failures = _validators
                    .Select(async v => await v.ValidateAsync(context, cancellationToken));

                var results = await Task.WhenAll(failures);
                var validationFailures = results.SelectMany(result => result.Errors)
                    .Where(f => f != null)
                    .ToList();

                if (validationFailures.Count != 0)
                {
                    throw new ValidationException(validationFailures);
                }
            }

            return await next();
        }
    }
}
