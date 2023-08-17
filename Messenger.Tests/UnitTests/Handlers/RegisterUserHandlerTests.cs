using AutoMapper;
using MediatR;
using Messenger.App;
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
    public class RegisterUserHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<AppDBContext> _dbContext;
        private readonly string existingUserEmail = "user1@email.pl";

        public RegisterUserHandlerTests()
        {
            var myProfile = new AppMappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            var mapper = new Mapper(configuration);

            _dbContext = new Mock<AppDBContext>();
            _dbContext.Setup(x => x.Users).ReturnsDbSet(DataHelpers.getTestUsers());
            _mapper = mapper;
        }

        [Fact]
        public async void Handle_Should_Return_Exception_When_Email_Is_Currently_In_Use()
        {
            RegisterUserCommand command = new RegisterUserCommand() { Email = existingUserEmail, Password = "pass", Name = "User1" };

            RegisterUserCommandHandler handler = new RegisterUserCommandHandler(_dbContext.Object, _mapper);

            var act = async () => await handler.Handle(command, default);
            ApplicationException exception = await Assert.ThrowsAsync<ApplicationException>(act);
            Assert.Equal("User with this email is already taken", exception.Message);
        }

        [Fact]
        public async void Handle_Should_Proceed_When_Correct_ConversationId_Provided()
        {
            RegisterUserCommand command = new RegisterUserCommand() { Email = "new@email.pl", Password = "pass", Name = "User1" };

            RegisterUserCommandHandler handler = new RegisterUserCommandHandler(_dbContext.Object, _mapper);

            var result = await handler.Handle(command, default);

            _dbContext.Verify(x => x.Users.AddAsync(It.IsAny<User>(), default), Times.Once());
            _dbContext.Verify(x => x.SaveChangesAsync(default), Times.Once());
        }
    }
}
