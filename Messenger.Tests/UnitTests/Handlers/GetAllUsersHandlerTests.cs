using AutoMapper;
using Messenger.App.Commands;
using Messenger.App.Handlers;
using Messenger.App;
using Messenger.Persistence.EF;
using Moq;
using Moq.EntityFrameworkCore;
using Messenger.App.Queries;

namespace Messenger.Tests.UnitTests.Handlers
{
    public class GetAllUsersHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<AppDBContext> _dbContext;

        public GetAllUsersHandlerTests()
        {
            var myProfile = new AppMappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            var mapper = new Mapper(configuration);

            _dbContext = new Mock<AppDBContext>();
            _dbContext.Setup(x => x.Users).ReturnsDbSet(DataHelpers.getTestUsers());
            _mapper = mapper;
        }

        [Fact]
        public async void Handle_Should_Proceed_When_Correct_Query_Provided()
        {
            GetAllUsersQuery command = new GetAllUsersQuery() { };

            GetAllUsersQueryHandler handler = new GetAllUsersQueryHandler(_dbContext.Object, _mapper);

            var result = await handler.Handle(command, default);

            Assert.NotNull(result);
            Assert.NotEmpty(result.Users);
        }
    }
}
