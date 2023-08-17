using AutoMapper;
using FluentAssertions.Primitives;
using MediatR;
using Messenger.App.Commands;
using Messenger.App.Handlers;
using Messenger.Persistence.EF;
using Messenger.Persistence.EF.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;

namespace Messenger.Tests.UnitTests.Handlers
{
    public class AddMessageHandlerTests
    {
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<AppDBContext> _dbContext;
        private readonly Guid existingConversationId = Guid.Parse("018ef796-9531-460e-85a9-025005cfa74b");
        private readonly Guid existingSenderId = Guid.Parse("7f6c1443-55b3-410a-aaa8-885b4d580b11");

        public AddMessageHandlerTests()
        {
            _dbContext = new Mock<AppDBContext>();
            _mapper = new Mock<IMapper>();

            _dbContext.Setup(x => x.Conversations).ReturnsDbSet(DataHelpers.GetTestConversations());
            _dbContext.Setup(x => x.Users).ReturnsDbSet(DataHelpers.getTestUsers());
            _dbContext.Setup(x => x.Messages).ReturnsDbSet(DataHelpers.getTestMessages());
        }

        [Fact]
        public async void Handle_Should_Return_Exception_When_Non_Existing_ConversationId_Provided()
        {
            AddMessageCommand command = new AddMessageCommand() { ConversationId = Guid.NewGuid(), Message = "test", SenderId = Guid.NewGuid() };

            AddMessageCommandHandler handler = new AddMessageCommandHandler(_dbContext.Object, _mapper.Object);

            var act = async () => await handler.Handle(command, default);
            Exception exception = await Assert.ThrowsAsync<Exception>(act);
            Assert.Equal("There is no conversation with given Id", exception.Message);
        }

        [Fact]
        public async void Handle_Should_Return_Exception_When_Non_Existing_SenderId_Provided()
        {
            AddMessageCommand command = new AddMessageCommand() { ConversationId = existingConversationId, Message = "test", SenderId = Guid.NewGuid() };

            AddMessageCommandHandler handler = new AddMessageCommandHandler(_dbContext.Object, _mapper.Object);

            var act = async () => await handler.Handle(command, default);
            Exception exception = await Assert.ThrowsAsync<Exception>(act);
            Assert.Equal("There is no user with given Id", exception.Message);
        }

        [Fact]
        public async void Handle_Should_Proceed_When_Correct_ConversationId_And_UserId_Provided()
        {
            AddMessageCommand command = new AddMessageCommand() { ConversationId = existingConversationId, Message = "test", SenderId = existingSenderId };

            AddMessageCommandHandler handler = new AddMessageCommandHandler(_dbContext.Object, _mapper.Object);

            var result = await handler.Handle(command, default);

            Assert.Equal(Unit.Value, result);
            _dbContext.Verify(x => x.Messages.AddAsync(It.IsAny<Message>(), default), Times.Once());
            _dbContext.Verify(x => x.Conversations.Update(It.IsAny<Conversation>()), Times.Once());
            _dbContext.Verify(x => x.SaveChangesAsync(default), Times.Once());

        }
    }
}

