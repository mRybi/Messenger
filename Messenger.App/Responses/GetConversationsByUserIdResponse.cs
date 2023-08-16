using Messenger.Persistence.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.App.Responses
{
    public class GetConversationsByUserIdResponse
    {
        public List<Conversation> Conversations { get; set; }

    }
}
