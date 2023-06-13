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
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, GetAllUsersResponse>
    {
        private readonly AppDBContext _context;

        public GetAllUsersQueryHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<GetAllUsersResponse> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var list = await _context.Users.AsNoTracking()
                .ToListAsync(cancellationToken);

            return new GetAllUsersResponse() { Users = list };
        }
    }
}
