using AutoMapper;
using MediatR;
using Messenger.App.Authorization;
using Messenger.App.Commands;
using Messenger.App.Responses;
using Messenger.Persistence.EF;
using Microsoft.EntityFrameworkCore;

namespace Messenger.App.Handlers
{
    public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, AuthenticateUserResponse>
    {
        private readonly AppDBContext _context;
        private readonly IMapper _mapper;
        private readonly IJwtUtils _jwtUtils;

        public AuthenticateUserCommandHandler(AppDBContext context, IMapper mapper, IJwtUtils jwtUtils)
        {
            _context = context;
            _mapper = mapper;
            _jwtUtils = jwtUtils;
        }
        public async Task<AuthenticateUserResponse> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Email == request.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.HashedPassword))
                throw new Exception("Username or password is incorrect");

            var response = _mapper.Map<AuthenticateUserResponse>(user);
            response.Token = _jwtUtils.GenerateToken(user);
            return response;
        }
    }
}


