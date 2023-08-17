using MediatR;
using Messenger.App.Commands;
using Messenger.Persistence.EF;
using Microsoft.EntityFrameworkCore;

namespace Messenger.App.Handlers
{
    public class RemoveUserFromConversationCommandHandler : IRequestHandler<RemoveUserFromConversationCommand, Unit>
    {
        private readonly AppDBContext _context;

        public RemoveUserFromConversationCommandHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(RemoveUserFromConversationCommand request, CancellationToken cancellationToken)
        {
            var converastion = await _context.Conversations.Where(x => x.Id == request.ConversationId).Include(x => x.Users).FirstOrDefaultAsync(cancellationToken);
            if (converastion == null)
            {
                throw new Exception("There is no conversation with given Id");
            }

            var isInConversation = converastion.Users.Any(x => x.Id == request.UserId);

            if (!isInConversation)
            {
                throw new Exception("User is not in this conversation");
            }

            var newUsers = converastion.Users.Where(x => x.Id != request.UserId);

            converastion.Users = newUsers.ToList();

            _context.Conversations.Update(converastion);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
