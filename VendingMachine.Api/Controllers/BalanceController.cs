using Microsoft.AspNetCore.Mvc;
using VendingMachine.Application.User.Commands.DepositBalance;
using VendingMachine.Application.User.Commands.RefundBaalance;
using VendingMachine.Application.User.Dtos;

namespace VendingMachine.Api.Controllers
{
    public class BalanceController : ApiController
    {
        [ProducesResponseType(typeof(UserBalanceDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("{userId}")]
        public async Task<ActionResult> Deposit([FromRoute] int userId, [FromBody] decimal depositAmount)
        {
            var request = new DepositBalanceCommand
            {
                UserId = userId,
                DepositAmount = depositAmount
            };

            var result = await Mediator.Send(request);

            return Ok(result);
        }

        [ProducesResponseType(typeof(UserBalanceRefundDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("{userId}")]
        public async Task<ActionResult> Refund([FromRoute] int userId)
        {
            var request = new RefundBalanceCommand
            {
                UserId = userId
            };

            var result = await Mediator.Send(request);

            return Ok(result);
        }
    }
}
