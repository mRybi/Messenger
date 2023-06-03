using MediatR;

namespace Messenger.App.Commands
{
    public class RegisterUserCommand : IRequest<Guid>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }

    }
}
