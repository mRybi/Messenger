using Messenger.Persistence.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.App.Responses
{
    public class GetConversationMessagesResponse
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastMessageAt { get; set; }
        public string Name { get; set; }
        public bool IsGroup { get; set; }
        public IEnumerable<Message> Messages { get; set; }
    }
}
