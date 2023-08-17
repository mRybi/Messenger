using AutoMapper;
using Messenger.Persistence.EF.Models;
using Messenger.Persistence.EF;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq.EntityFrameworkCore;
using MediatR;
using Messenger.App.Commands;
using Messenger.App.Handlers;

namespace Messenger.Tests.UnitTests.Handlers
{
    public class AddUserToConversationHandlerTests
    {
        private readonly Mock<AppDBContext> _dbContext;
        private readonly Guid existingConversationId = Guid.Parse("018ef796-9531-460e-85a9-025005cfa74b");

        public AddUserToConversationHandlerTests()
        {
            _dbContext = new Mock<AppDBContext>();

            _dbContext.Setup(x => x.Conversations).ReturnsDbSet(DataHelpers.GetTestConversations());
            _dbContext.Setup(x => x.Users).ReturnsDbSet(DataHelpers.getTestUsers());
            _dbContext.Setup(x => x.Messages).ReturnsDbSet(DataHelpers.getTestMessages());
        }

        [Fact]
        public async void Handle_Should_Return_Exception_When_Non_Existing_ConversationId_Provided()
        {
            AddUserToConversationCommand command = new AddUserToConversationCommand() { ConversationId = Guid.NewGuid(), UserIds = new List<Guid>() { Guid.NewGuid() } };

            AddUserToConversationCommandHandler handler = new AddUserToConversationCommandHandler(_dbContext.Object);

            var act = async () => await handler.Handle(command, default);
            Exception exception = await Assert.ThrowsAsync<Exception>(act);
            Assert.Equal("There is no conversation with given Id", exception.Message);
        }

        [Fact]
        public async void Handle_Should_Proceed_When_Correct_ConversationId_And_UserId_Provided()
        {
            AddUserToConversationCommand command = new AddUserToConversationCommand() { ConversationId = existingConversationId, UserIds = new List<Guid>() { Guid.NewGuid() } };

            AddUserToConversationCommandHandler handler = new AddUserToConversationCommandHandler(_dbContext.Object);

            var result = await handler.Handle(command, default);

            Assert.Equal(Unit.Value, result);
            _dbContext.Verify(x => x.Conversations.Update(It.IsAny<Conversation>()), Times.Once());
            _dbContext.Verify(x => x.SaveChangesAsync(default), Times.Once());

        }
    }
}
