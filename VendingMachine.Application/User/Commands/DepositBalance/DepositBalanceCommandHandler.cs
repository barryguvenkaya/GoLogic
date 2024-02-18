using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VendingMachine.Application.Common.Interfaces;
using VendingMachine.Application.User.Dtos;

namespace VendingMachine.Application.User.Commands.DepositBalance
{
    public class DepositBalanceCommandHandler : IRequestHandler<DepositBalanceCommand, UserBalanceDto>
    {
        private readonly IUserRepository _userContext;
        private readonly IMapper _mapper;
        private readonly ILogger<DepositBalanceCommandHandler> _logger;

        public DepositBalanceCommandHandler(
            IUserRepository userContext,
            IMapper mapper,
            ILogger<DepositBalanceCommandHandler> logger
            )
        {
            _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<UserBalanceDto> Handle(DepositBalanceCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);
            ArgumentNullException.ThrowIfNull(cancellationToken);

            _logger.LogInformation("Handling DepositBalanceCommand for user id {UserId} for the deposit amount of {DepositAmount}", request.UserId, request.DepositAmount);

            var user = await _userContext.GetUserByIdAsync(request.UserId, cancellationToken);

            if (user == null)
            {
                throw new Exception($"User {request.UserId} not found");
            }

            user.Balance += request.DepositAmount;

            var updatedUser = await _userContext.UpdateUserAsync(user, cancellationToken);

            return _mapper.Map<UserBalanceDto>(updatedUser);
        }
    }
}
