using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VendingMachine.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ApiController : ControllerBase
    {
        protected IMediator Mediator => HttpContext.RequestServices.GetService<IMediator>();

        // TODO: To be production ready, implement pagination logic here.
    }
}
