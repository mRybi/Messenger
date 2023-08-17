using AutoMapper;
using MediatR;
using Messenger.App.Authorization;
using Messenger.App.Commands;
using Messenger.App.Handlers;
using Messenger.App.Responses;
using Messenger.Persistence.EF;
using Messenger.Persistence.EF.Models;
using Moq;
using Moq.EntityFrameworkCore;

namespace Messenger.Tests.UnitTests.Handlers
{
    public class CreateConversationHandlerTests
    {
        private readonly Mock<AppDBContext> _dbContext;

        public CreateConversationHandlerTests()
        {
            _dbContext = new Mock<AppDBContext>();

            _dbContext.Setup(x => x.Conversations).ReturnsDbSet(DataHelpers.GetTestConversations());
            _dbContext.Setup(x => x.Users).ReturnsDbSet(DataHelpers.getTestUsers());
        }

        [Fact]
        public async void Handle_Should_Proceed_When_Correct_Command_Provided()
        {
            CreateConversationCommand command = new CreateConversationCommand() { Name = "convo 1", IsGroup = false, UserIds = new List<Guid>() { Guid.NewGuid(), Guid.NewGuid() } };

            CreateConversationCommandHandler handler = new CreateConversationCommandHandler(_dbContext.Object);

            var result = await handler.Handle(command, default);

            Assert.IsNotType<Exception>(result);
            Assert.IsType<Guid>(result);

            _dbContext.Verify(x => x.Conversations.AddAsync(It.IsAny<Conversation>(), default), Times.Once());
            _dbContext.Verify(x => x.SaveChangesAsync(default), Times.Once());
        }
    }
}
