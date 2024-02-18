using Microsoft.AspNetCore.Mvc;
using VendingMachine.Application.Transaction.Commands.Purchase;
using VendingMachine.Application.Transaction.Dtos;
using VendingMachine.Application.Transaction.Queries;

namespace VendingMachine.Api.Controllers
{
    public class TransactionController : ApiController
    {
        [ProducesResponseType(typeof(PurchaseResultDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("{userId}")]
        public async Task<ActionResult> Purchase([FromRoute] int userId, [FromBody] PurchaseCommand request)
        {
            request.UserId = userId;

            var result = await Mediator.Send(request);

            return Ok(result);
        }

        [ProducesResponseType(typeof(List<TransactionDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{userId}")]
        public async Task<ActionResult<List<TransactionDto>>> Get([FromRoute] int userId, CancellationToken cancellationToken = default)
        {
            var result = await Mediator.Send(new GetTransactionsByUserIdQuery(userId), cancellationToken);
            return Ok(result);
        } 
    }
}
