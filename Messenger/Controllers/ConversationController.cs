using MediatR;
using Messenger.App.Authorization;
using Messenger.App.Commands;
using Messenger.App.Queries;
using Messenger.App.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Messenger.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ConversationController : Controller
    {
        private readonly ISender _mediator;
        public ConversationController(ISender mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("create")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        public async Task<ActionResult> Post(CreateConversationCommand command)
        {
            var createdRecordId = await _mediator.Send(command);

            return Created(nameof(Post), createdRecordId);
        }


        [AllowAnonymous]
        [HttpPost("addUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> AddUser(AddUserToConversationCommand command)
        {
            await _mediator.Send(command);

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("removeUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> RemoveUser(RemoveUserFromConversationCommand command)
        {
            await _mediator.Send(command);

            return Ok();
        }

        [AllowAnonymous]
        [HttpDelete("deleteConversation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteConversation(DeleteConversationCommand command)
        {
            await _mediator.Send(command);

            return Ok();
        }

        [AllowAnonymous]
        [HttpGet("getMessages")]
        [ProducesResponseType(typeof(GetConversationMessagesResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult> Get([FromQuery] GetConversationMessagesQuery command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("getByUserId")]
        [ProducesResponseType(typeof(GetConversationsByUserIdResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetByUserId([FromQuery] GetConversationsByUserIdQuery command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }
    }
}

