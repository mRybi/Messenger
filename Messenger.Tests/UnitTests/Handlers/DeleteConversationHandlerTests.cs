using MediatR;
using Messenger.App.Commands;
using Messenger.App.Handlers;
using Messenger.Persistence.EF;
using Messenger.Persistence.EF.Models;
using Moq;
using Moq.EntityFrameworkCore;

namespace Messenger.Tests.UnitTests.Handlers
{
    public class DeleteConversationHandlerTests
    {
        private readonly Mock<AppDBContext> _dbContext;
        private readonly Guid existingConversationId = Guid.Parse("018ef796-9531-460e-85a9-025005cfa74b");

        public DeleteConversationHandlerTests()
        {
            _dbContext = new Mock<AppDBContext>();

            _dbContext.Setup(x => x.Conversations).ReturnsDbSet(DataHelpers.GetTestConversations());
        }

        [Fact]
        public async void Handle_Should_Return_Exception_When_Non_Existing_ConversationId_Provided()
        {
            DeleteConversationCommand command = new DeleteConversationCommand() { Id = Guid.NewGuid() };

            DeleteConversationCommandHandler handler = new DeleteConversationCommandHandler(_dbContext.Object);

            var act = async () => await handler.Handle(command, default);
            Exception exception = await Assert.ThrowsAsync<Exception>(act);
            Assert.Equal("There is no conversation with given Id", exception.Message);
        }

        [Fact]
        public async void Handle_Should_Proceed_When_Correct_ConversationId_Provided()
        {
            DeleteConversationCommand command = new DeleteConversationCommand() { Id = existingConversationId };

            DeleteConversationCommandHandler handler = new DeleteConversationCommandHandler(_dbContext.Object);

            var result = await handler.Handle(command, default);

            Assert.Equal(Unit.Value, result);
            _dbContext.Verify(x => x.Conversations.Remove(It.IsAny<Conversation>()), Times.Once());
            _dbContext.Verify(x => x.SaveChangesAsync(default), Times.Once());

        }
    }
}
