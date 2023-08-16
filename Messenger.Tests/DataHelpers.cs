using Messenger.Persistence.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Tests
{
    public static class DataHelpers
    {
        public static List<Conversation> GetTestConversations()
        {
            var testConversations = new List<Conversation>();

            testConversations.Add(new Conversation { Id = Guid.NewGuid(), Name = "Demo1", CreatedAt = DateTime.Now, IsGroup = false, LastMessageAt = DateTime.Now, Messages = getTestMessages(), Users = getTestUsers() });
            testConversations.Add(new Conversation { Id = Guid.NewGuid(), Name = "Demo2", CreatedAt = DateTime.Now, IsGroup = false, LastMessageAt = DateTime.Now, Messages = getTestMessages(), Users = getTestUsers() });
            testConversations.Add(new Conversation { Id = Guid.NewGuid(), Name = "Demo3", CreatedAt = DateTime.Now, IsGroup = false, LastMessageAt = DateTime.Now, Messages = getTestMessages(), Users = getTestUsers() });
            testConversations.Add(new Conversation { Id = Guid.NewGuid(), Name = "Demo4", CreatedAt = DateTime.Now, IsGroup = false, LastMessageAt = DateTime.Now, Messages = getTestMessages(), Users = getTestUsers() });
            testConversations.Add(new Conversation { Id = Guid.NewGuid(), Name = "Demo5", CreatedAt = DateTime.Now, IsGroup = false, LastMessageAt = DateTime.Now, Messages = getTestMessages(), Users = getTestUsers() });
            testConversations.Add(new Conversation { Id = Guid.NewGuid(), Name = "Demo6", CreatedAt = DateTime.Now, IsGroup = false, LastMessageAt = DateTime.Now, Messages = getTestMessages(), Users = getTestUsers() });

            return testConversations;
        }

        public static List<User> getTestUsers()
        {
            var users = new List<User>();


            users.Add(new User { Id = Guid.NewGuid(), Name = "User1", Email = "user1@email.pl", EmailVerified = DateTime.Now, Image = "no img", CreatedAt = DateTime.Now, HashedPassword = null, Conversations = new List<Conversation>(), UpdatedAt = DateTime.Now });
            users.Add(new User { Id = Guid.NewGuid(), Name = "User2", Email = "user2@email.pl", EmailVerified = DateTime.Now, Image = "no img", CreatedAt = DateTime.Now, HashedPassword = null, Conversations = new List<Conversation>(), UpdatedAt = DateTime.Now });
            users.Add(new User { Id = Guid.NewGuid(), Name = "User3", Email = "user3@email.pl", EmailVerified = DateTime.Now, Image = "no img", CreatedAt = DateTime.Now, HashedPassword = null, Conversations = new List<Conversation>(), UpdatedAt = DateTime.Now });
            users.Add(new User { Id = Guid.NewGuid(), Name = "User4", Email = "user4@email.pl", EmailVerified = DateTime.Now, Image = "no img", CreatedAt = DateTime.Now, HashedPassword = null, Conversations = new List<Conversation>(), UpdatedAt = DateTime.Now });
            users.Add(new User { Id = Guid.NewGuid(), Name = "User5", Email = "user5@email.pl", EmailVerified = DateTime.Now, Image = "no img", CreatedAt = DateTime.Now, HashedPassword = null, Conversations = new List<Conversation>(), UpdatedAt = DateTime.Now });
            users.Add(new User { Id = Guid.NewGuid(), Name = "User6", Email = "user6@email.pl", EmailVerified = DateTime.Now, Image = "no img", CreatedAt = DateTime.Now, HashedPassword = null, Conversations = new List<Conversation>(), UpdatedAt = DateTime.Now });

            return users;
        }

        public static List<Message> getTestMessages()
        {
            var messages = new List<Message>();
            var user1 = new User { Id = Guid.NewGuid(), Name = "User1", Email = "user1@email.pl", EmailVerified = DateTime.Now, Image = "no img", CreatedAt = DateTime.Now, HashedPassword = null, Conversations = new List<Conversation>(), UpdatedAt = DateTime.Now };
            var user2 = new User { Id = Guid.NewGuid(), Name = "User2", Email = "user2@email.pl", EmailVerified = DateTime.Now, Image = "no img", CreatedAt = DateTime.Now, HashedPassword = null, Conversations = new List<Conversation>(), UpdatedAt = DateTime.Now };

            messages.Add(new Message { Id = Guid.NewGuid(), Body = "test", CreatedAt = DateTime.Now, Image = "no img", Sender = user1 });
            messages.Add(new Message { Id = Guid.NewGuid(), Body = "test", CreatedAt = DateTime.Now, Image = "no img", Sender = user2 });
            messages.Add(new Message { Id = Guid.NewGuid(), Body = "test", CreatedAt = DateTime.Now, Image = "no img", Sender = user2 });
            messages.Add(new Message { Id = Guid.NewGuid(), Body = "test", CreatedAt = DateTime.Now, Image = "no img", Sender = user1 });

            return messages;
        }
    }
}
