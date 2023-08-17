using AutoMapper;
using MediatR;
using Messenger.App.Commands;
using Messenger.Persistence.EF;
using Messenger.Persistence.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.App.Handlers
{
    public class CreateConversationCommandHandler : IRequestHandler<CreateConversationCommand, Guid>
    {
        private readonly AppDBContext _context;

        public CreateConversationCommandHandler(AppDBContext context)
        {
            _context = context;
        }
        public async Task<Guid> Handle(CreateConversationCommand request, CancellationToken cancellationToken)
        {
            var conversationsUsers = _context.Users.Where(x => request.UserIds.Contains(x.Id)).ToList();
            var conversation = new Conversation();

            conversation.Name = request.Name;
            conversation.IsGroup = request.IsGroup;
            conversation.LastMessageAt = DateTime.UtcNow;
            conversation.CreatedAt= DateTime.UtcNow;
            conversation.Messages = new List<Message>();
            conversation.Users = conversationsUsers;

            await _context.Conversations.AddAsync(conversation);
            await _context.SaveChangesAsync(cancellationToken);

            return conversation.Id;
        }
    }
}
