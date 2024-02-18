using MediatR;
using Microsoft.Extensions.Logging;

namespace VendingMachine.Application.Behaviours
{
    public class UnhandledExceptionBehaviour<TRequest, TResponse>(ILogger<TRequest> logger) : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<TRequest> _logger = logger;

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                // Log level can change per custom exception types here
                LogException(ex, LogLevel.Error, request);
                throw;
            }
        }

        private void LogException(Exception ex, LogLevel level, TRequest request)
        {
            _logger.Log(
                level,
                ex,
                "InfoTrack.Common Request: {ExceptionType} for Request {Name} {@Request}",
                ex.GetType().Name,
                typeof(TRequest).Name,
                request);
        }
    }
}