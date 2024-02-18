using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VendingMachine.Application.Common.Interfaces;
using VendingMachine.Application.User.Dtos;

namespace VendingMachine.Application.User.Commands.RefundBaalance
{
    public class RefundBalanceCommandHandler : IRequestHandler<RefundBalanceCommand, UserBalanceRefundDto>
    {
        private readonly IUserRepository _userContext;
        private readonly ILogger<RefundBalanceCommandHandler> _logger;

        public RefundBalanceCommandHandler(
            IUserRepository userContext,
            ILogger<RefundBalanceCommandHandler> logger
            )
        {
            _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<UserBalanceRefundDto> Handle(RefundBalanceCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);
            ArgumentNullException.ThrowIfNull(cancellationToken);

            _logger.LogInformation("Handling RefundBalanceCommand for user id {UserId}.", request.UserId);

            var user = await _userContext.GetUserByIdAsync(request.UserId, cancellationToken);

            if (user == null)
            {
                throw new Exception($"User {request.UserId} not found");
            }

            var refundAmount = user.Balance;

            user.Balance = 0;

            await _userContext.UpdateUserAsync(user, cancellationToken);

            return new UserBalanceRefundDto { ChangeAmount = refundAmount };
        }
    }
}
