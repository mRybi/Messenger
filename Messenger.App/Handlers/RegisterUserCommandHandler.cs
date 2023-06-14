using AutoMapper;
using MediatR;
using Messenger.App.Commands;
using Messenger.Persistence.EF;
using Messenger.Persistence.EF.Models;

namespace Messenger.App.Handlers
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Guid>
    {
        private readonly AppDBContext _context;
        private readonly IMapper _mapper;

        public RegisterUserCommandHandler(AppDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            if (_context.Users.Any(x => x.Email == request.Email))
                throw new ApplicationException($"User with this email is already taken");

            var user = _mapper.Map<User>(request);

            user.CreatedAt = DateTime.Now;
            user.UpdatedAt = DateTime.Now;
            user.Image = "image";
            user.HashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync(cancellationToken);

            return user.Id;
        }

    }
}
