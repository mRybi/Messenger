using AutoMapper;
using MediatR;
using Messenger.App.Commands;
using Messenger.Persistence.EF;
using Microsoft.EntityFrameworkCore;

namespace Messenger.App.Handlers
{
    public class AddUserToConversationCommandHandler : IRequestHandler<AddUserToConversationCommand, Unit>
    {
        private readonly AppDBContext _context;
        private readonly IMapper _mapper;

        public AddUserToConversationCommandHandler(AppDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(AddUserToConversationCommand request, CancellationToken cancellationToken)
        {
            var converastion = await _context.Conversations.Where(x => x.Id == request.ConversationId).Include(x => x.Users).FirstOrDefaultAsync(cancellationToken);
            if (converastion == null)
            {
                throw new Exception("There is no conversation with given Id");
            }

            var newUsers = _context.Users.Where(x => request.UserIds.Contains(x.Id));

            converastion.Users = converastion.Users.Concat(newUsers);

            _context.Conversations.Update(converastion);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
