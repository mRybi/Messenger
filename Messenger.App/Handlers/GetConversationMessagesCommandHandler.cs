using AutoMapper;
using MediatR;
using Messenger.App.Queries;
using Messenger.App.Responses;
using Messenger.Persistence.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.App.Handlers
{
    public class GetConversationMessagesQueryHandler : IRequestHandler<GetConversationMessagesQuery, GetConversationMessagesResponse>
    {
        private readonly AppDBContext _context;
        private readonly IMapper _mapper;

        public GetConversationMessagesQueryHandler(AppDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetConversationMessagesResponse> Handle(GetConversationMessagesQuery request, CancellationToken cancellationToken)
        {
            var converastion = await _context.Conversations.Where(x => x.Id == request.Id)
                .Include(x => x.Messages)
                .ThenInclude(x => x.Sender).FirstOrDefaultAsync(cancellationToken);

            if (converastion == null)
            {
                throw new Exception("There is no conversation with given Id");
            }

            var result = _mapper.Map<GetConversationMessagesResponse>(converastion);

            return result;
        }
    }
}
