using AutoMapper;
using MediatR;
using Messenger.App.Queries;
using Messenger.App.Responses;
using Messenger.Persistence.EF;
using Microsoft.EntityFrameworkCore;

namespace Messenger.App.Handlers
{
    public class GetConversationsByUserIdQueryHandler : IRequestHandler<GetConversationsByUserIdQuery, GetConversationsByUserIdResponse>
    {
        private readonly AppDBContext _context;

        public GetConversationsByUserIdQueryHandler(AppDBContext context)
        {
            _context = context;
        }
        public async Task<GetConversationsByUserIdResponse> Handle(GetConversationsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Conversations.Where(x => x.Users.Any(x => x.Id == request.Id))
                .Include(x => x.Users)
                //.Include(x => x.Messages)
                .ToListAsync();
        
            return new GetConversationsByUserIdResponse() { Conversations = result };
        }
    }
}
