using AutoMapper;
using MediatR;
using Messenger.App.Commands;
using Messenger.Persistence.EF;
using Microsoft.EntityFrameworkCore;

namespace Messenger.App.Handlers
{
    public class DeleteConversationCommandHandler : IRequestHandler<DeleteConversationCommand, Unit>
    {
        private readonly AppDBContext _context;

        public DeleteConversationCommandHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteConversationCommand request, CancellationToken cancellationToken)
        {
            var converastion = await _context.Conversations.Where(x => x.Id == request.Id).Include(x => x.Users).FirstOrDefaultAsync(cancellationToken);
            if (converastion == null)
            {
                throw new Exception("There is no conversation with given Id");
            }

            _context.Conversations.Remove(converastion);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
