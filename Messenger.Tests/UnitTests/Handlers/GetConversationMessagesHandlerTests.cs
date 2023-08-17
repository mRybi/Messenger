using AutoMapper;
using Messenger.App.Handlers;
using Messenger.App.Queries;
using Messenger.App;
using Messenger.Persistence.EF;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq.EntityFrameworkCore;
using Messenger.App.Commands;

namespace Messenger.Tests.UnitTests.Handlers
{
    public class GetConversationMessagesHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<AppDBContext> _dbContext;
        private readonly Guid existingConversationId = Guid.Parse("018ef796-9531-460e-85a9-025005cfa74b");

        public GetConversationMessagesHandlerTests()
        {
            var myProfile = new AppMappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            var mapper = new Mapper(configuration);

            _dbContext = new Mock<AppDBContext>();
            _dbContext.Setup(x => x.Conversations).ReturnsDbSet(DataHelpers.GetTestConversations());
            _mapper = mapper;
        }

        [Fact]
        public async void Handle_Should_Return_Exception_When_Non_Existing_ConversationId_Provided()
        {
            GetConversationMessagesQuery query = new GetConversationMessagesQuery() { Id = Guid.NewGuid() };

            GetConversationMessagesQueryHandler handler = new GetConversationMessagesQueryHandler(_dbContext.Object, _mapper);

            var act = async () => await handler.Handle(query, default);
            Exception exception = await Assert.ThrowsAsync<Exception>(act);
            Assert.Equal("There is no conversation with given Id", exception.Message);
        }

        [Fact]
        public async void Handle_Should_Proceed_When_Correct_Query_Provided()
        {
            GetConversationMessagesQuery query = new GetConversationMessagesQuery() { Id = existingConversationId };

            GetConversationMessagesQueryHandler handler = new GetConversationMessagesQueryHandler(_dbContext.Object, _mapper);

            var result = await handler.Handle(query, default);

            Assert.NotNull(result);
            Assert.NotEmpty(result.Messages);
        }
    }
}
