using Messenger.Persistence.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Messenger.App.Responses
{
    public class GetAllUsersResponse
    {
        public IEnumerable<User> Users { get; set; }
    }
}
