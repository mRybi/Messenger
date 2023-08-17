using MediatR;
using Messenger.App.Commands;
using Messenger.App.Handlers;
using Messenger.Persistence.EF;
using Messenger.Persistence.EF.Models;
using Moq;
using Moq.EntityFrameworkCore;

namespace Messenger.Tests.UnitTests.Handlers
{
    public class RemoveUserFromConversationHandlerTests
    {
        private readonly Mock<AppDBContext> _dbContext;
        private readonly Guid existingConversationId = Guid.Parse("018ef796-9531-460e-85a9-025005cfa74b");
        private readonly Guid existingUserId = Guid.Parse("7f6c1443-55b3-410a-aaa8-885b4d580b11");

        public RemoveUserFromConversationHandlerTests()
        {
            _dbContext = new Mock<AppDBContext>();

            _dbContext.Setup(x => x.Conversations).ReturnsDbSet(DataHelpers.GetTestConversations());
            _dbContext.Setup(x => x.Users).ReturnsDbSet(DataHelpers.getTestUsers());
            _dbContext.Setup(x => x.Messages).ReturnsDbSet(DataHelpers.getTestMessages());
        }

        [Fact]
        public async void Handle_Should_Return_Exception_When_Non_Existing_ConversationId_Provided()
        {
            RemoveUserFromConversationCommand command = new RemoveUserFromConversationCommand() { ConversationId = Guid.NewGuid(), UserId = Guid.NewGuid() };

            RemoveUserFromConversationCommandHandler handler = new RemoveUserFromConversationCommandHandler(_dbContext.Object);

            var act = async () => await handler.Handle(command, default);
            Exception exception = await Assert.ThrowsAsync<Exception>(act);
            Assert.Equal("There is no conversation with given Id", exception.Message);
        }

        [Fact]
        public async void Handle_Should_Return_Exception_When_Non_Existing_SenderId_Provided()
        {
            RemoveUserFromConversationCommand command = new RemoveUserFromConversationCommand() { ConversationId = existingConversationId, UserId = Guid.NewGuid() };

            RemoveUserFromConversationCommandHandler handler = new RemoveUserFromConversationCommandHandler(_dbContext.Object);

            var act = async () => await handler.Handle(command, default);
            Exception exception = await Assert.ThrowsAsync<Exception>(act);
            Assert.Equal("User is not in this conversation", exception.Message);
        }

        [Fact]
        public async void Handle_Should_Proceed_When_Correct_ConversationId_And_UserId_Provided()
        {
            RemoveUserFromConversationCommand command = new RemoveUserFromConversationCommand() { ConversationId = existingConversationId, UserId = existingUserId };

            RemoveUserFromConversationCommandHandler handler = new RemoveUserFromConversationCommandHandler(_dbContext.Object);

            var result = await handler.Handle(command, default);

            Assert.Equal(Unit.Value, result);
            _dbContext.Verify(x => x.Conversations.Update(It.IsAny<Conversation>()), Times.Once());
            _dbContext.Verify(x => x.SaveChangesAsync(default), Times.Once());

        }
    }
}
