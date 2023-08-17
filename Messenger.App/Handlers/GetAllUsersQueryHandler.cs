using AutoMapper;
using MediatR;
using Messenger.App.Dtos;
using Messenger.App.Queries;
using Messenger.App.Responses;
using Messenger.Persistence.EF;
using Messenger.Persistence.EF.Models;
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
        private readonly IMapper _mapper;

        public GetAllUsersQueryHandler(AppDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetAllUsersResponse> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var list = await _context.Users.AsNoTracking()
            .ToListAsync(cancellationToken);

            var response = _mapper.Map<List<User>, List<UserDto>>(list);

            return new GetAllUsersResponse() { Users = response };
        }
    }
}
