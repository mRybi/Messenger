using Messenger.App.Commands;
using Messenger.Persistence.EF;
using Messenger.Persistence.EF.Models;

namespace Messenger.App.Services
{
    public interface IUserService
    {
        void Register(RegisterUserCommand registerUserCommand);
    }

    public class UserService : IUserService
    {
        public AppDBContext _context { get; set; }
        public UserService(AppDBContext context)
        {
            _context = context;
        }
        public void Register(RegisterUserCommand registerUserCommand)
        {
            if (_context.Users.Any(x => x.Email == registerUserCommand.Email))
                throw new ApplicationException($"User with this email is already taken");

            //var user = _mapper.Map<User>(model); dodac automappera

            var user = new User();
            user.Email = registerUserCommand.Email;
            user.Name = registerUserCommand.Name;
             
            user.CreatedAt = DateTime.Now;
            user.UpdatedAt = DateTime.Now;

            // hash password
            user.HashedPassword = BCrypt.Net.BCrypt.HashPassword(registerUserCommand.Password);

            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
}
