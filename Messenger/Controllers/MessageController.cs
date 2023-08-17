using MediatR;
using Messenger.App.Authorization;
using Messenger.App.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Messenger.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : Controller
    {
        private readonly ISender _mediator;
        public MessageController(ISender mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("add")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        public async Task<ActionResult> Add(AddMessageCommand command)
        {
            var createdRecordId = await _mediator.Send(command);

            return Created(nameof(Add), createdRecordId);
        }
    }
}
