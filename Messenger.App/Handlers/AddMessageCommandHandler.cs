using AutoMapper;
using MediatR;
using Messenger.App.Commands;
using Messenger.Persistence.EF;
using Messenger.Persistence.EF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.App.Handlers
{
    public class AddMessageCommandHandler : IRequestHandler<AddMessageCommand, Unit>
    {
        private readonly AppDBContext _context;
        private readonly IMapper _mapper;

        public AddMessageCommandHandler(AppDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(AddMessageCommand request, CancellationToken cancellationToken)
        {
            var converastion = await _context.Conversations.FirstOrDefaultAsync(x => x.Id == request.ConversationId);
            if (converastion == null)
            {
                throw new Exception("There is no conversation with given Id");
            }

            var newMessage = new Message();

            var sender = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.SenderId);
            newMessage.Sender = sender;

            newMessage.CreatedAt = DateTime.Now;
            newMessage.Body = request.Message;
            newMessage.Image = "image- not implemented";

            if(converastion.Messages == null)
            {
                converastion.Messages = new List<Message>() { newMessage };
            } else
            {
                converastion.Messages = converastion.Messages.Append(newMessage);
            }

            await _context.Messages.AddAsync(newMessage);
            _context.Conversations.Update(converastion);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
