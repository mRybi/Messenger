using MediatR;
using Messenger.App.Commands;
using Messenger.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Messenger.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ISender _mediator;
        public UserController(ILogger<WeatherForecastController> logger, ISender mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        public async Task<ActionResult> Post(RegisterUserCommand command)
        {
            var createdRecordId = await _mediator.Send(command);

            return Created(nameof(Post), createdRecordId);
        }
    }
}
