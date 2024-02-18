using Microsoft.AspNetCore.Mvc;
using VendingMachine.Application.Product.Dtos;
using VendingMachine.Application.Product.Queries.GetAllProducts;
using VendingMachine.Application.Product.Queries.GetProductById;

namespace VendingMachine.Api.Controllers
{
    public class ProductController : ApiController
    {
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{productId}")]
        public async Task<ActionResult<ProductDto>> Get([FromRoute] int productId, CancellationToken cancellationToken = default)
        {
            var result = await Mediator.Send(new GetProductByIdQuery(productId), cancellationToken);
            return Ok(result);
        }

        [ProducesResponseType(typeof(List<ProductDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> Get(CancellationToken cancellationToken)
        {
            // TODO: implement pagination to be production ready. Below to be added to the method signature [FromQuery] and to the query object. 
            // * limit
            // * offset

            var result = await Mediator.Send(new GetAllProductsQuery { }, cancellationToken);

            // TODO: Implement creation of pagination links within ApiController. Result object could contain that as a property which would be populated before returning result.

            return Ok(result);
        }
    }
}
