using Messenger.App.Commands;
using Messenger.App.Handlers;
using Messenger.App.Queries;
using Messenger.Persistence.EF;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Tests.UnitTests.Handlers
{
    public class GetConversationByUserIdHandlerTests
    {
        private readonly Mock<AppDBContext> _dbContext;
        private readonly Guid existingUserId = Guid.Parse("7f6c1443-55b3-410a-aaa8-885b4d580b11");

        public GetConversationByUserIdHandlerTests()
        {
            _dbContext = new Mock<AppDBContext>();

            _dbContext.Setup(x => x.Conversations).ReturnsDbSet(DataHelpers.GetTestConversations());
            _dbContext.Setup(x => x.Users).ReturnsDbSet(DataHelpers.getTestUsers());
        }

        [Fact]
        public async void Handle_Should_Proceed_When_Correct_Command_Provided()
        {
            GetConversationsByUserIdQuery query = new GetConversationsByUserIdQuery() { Id = existingUserId };

            GetConversationsByUserIdQueryHandler handler = new GetConversationsByUserIdQueryHandler(_dbContext.Object);

            var result = await handler.Handle(query, default);

            Assert.IsNotType<Exception>(result);
            Assert.NotEmpty(result.Conversations);

        }
    }
}
