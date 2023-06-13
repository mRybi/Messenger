using Messenger.App.Commands;
using Messenger.Persistence.EF;
using Messenger.Persistence.EF.Models;

namespace Messenger.App.Services
{
    public interface IUserService
    {
        User GetById(int id);
    }

    public class UserService : IUserService
    {
        public AppDBContext _context { get; set; }
        public UserService(AppDBContext context)
        {
            _context = context;
        }
        public User GetById(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) throw new KeyNotFoundException("User not found");
            return user;
        }
    }
}
