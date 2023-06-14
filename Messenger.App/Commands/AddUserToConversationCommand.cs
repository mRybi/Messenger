using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.App.Commands
{
    public class AddUserToConversationCommand : IRequest<Unit>
    {
        public Guid ConversationId { get; set; }
        public IEnumerable<Guid> UserIds { get; set; }
    }
}
