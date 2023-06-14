using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.App.Commands
{
    public class CreateConversationCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public bool IsGroup { get; set; }
        public IEnumerable<Guid> UserIds { get; set; }
    }
}
