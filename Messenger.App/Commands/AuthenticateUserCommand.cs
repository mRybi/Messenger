using MediatR;
using Messenger.App.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.App.Commands
{
    public class AuthenticateUserCommand : IRequest<AuthenticateUserResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
