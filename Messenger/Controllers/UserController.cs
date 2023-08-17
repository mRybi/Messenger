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
    public class UserController : Controller
    {
        private readonly ISender _mediator;
        public UserController(ISender mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        public async Task<ActionResult> Post(RegisterUserCommand command)
        {
            var createdRecordId = await _mediator.Send(command);

            return Created(nameof(Post), createdRecordId);
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        [ProducesResponseType(typeof(AuthenticateUserResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult> Authenticate(AuthenticateUserCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("logout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Logout(LogoutUserCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpGet("GetAllUsers")]
        [ProducesResponseType(typeof(GetAllUsersResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllUsers()
        {
            var response = await _mediator.Send(new GetAllUsersQuery());

            return Ok(response);
        }
    }
}
