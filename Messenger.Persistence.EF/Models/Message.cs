using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Persistence.EF.Models
{
    public class Message
    {
        public Guid Id { get; set; }
        public string Body { get; set; }
        public string Image { get; set; }
        public DateTime CreatedAt { get; set; }
        public User Sender { get; set; }
    }
}
