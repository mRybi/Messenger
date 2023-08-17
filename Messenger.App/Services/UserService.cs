using Messenger.App.Commands;
using Messenger.Persistence.EF;
using Messenger.Persistence.EF.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace Messenger.App.Services
{
    public interface IUserService
    {
        User GetById(string id);
    }

    public class UserService : IUserService
    {
        public AppDBContext _context { get; set; }
        public UserService(AppDBContext context)
        {
            _context = context;
        }
        public User GetById(string id)
        {
            var user = _context.Users.First(x => x.Id.ToString() == id);
            if (user == null) throw new KeyNotFoundException("User not found");
            return user;
        }
    }
}
