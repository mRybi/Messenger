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
    public class RegisterUserHandlerTests
    {
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<AppDBContext> _dbContext;
        private readonly string existingUserEmail = "user1@email.pl";

        public RegisterUserHandlerTests()
        {
            _dbContext = new Mock<AppDBContext>();
            _mapper = new Mock<IMapper>();

            _mapper.Setup(x => x.Map<User, AuthenticateUserResponse>(It.IsAny<User>())).Returns(new AuthenticateUserResponse());
            _dbContext.Setup(x => x.Users).ReturnsDbSet(DataHelpers.getTestUsers());


            //var stubUser = new User();
            //var stubUserDto = new AuthenticateUserResponse();
            //var mockMapper = new Mock<IMapper>();
            //_mapper.Setup(mock => mock.Map<User>(It.IsAny<AuthenticateUserResponse>())).Returns(stubUser);
        }

        [Fact]
        public async void Handle_Should_Return_Exception_When_Email_Is_Currently_In_Use()
        {
            RegisterUserCommand command = new RegisterUserCommand() { Email = existingUserEmail, Password = "pass", Name = "User1" };

            RegisterUserCommandHandler handler = new RegisterUserCommandHandler(_dbContext.Object, _mapper.Object);

            var act = async () => await handler.Handle(command, default);
            ApplicationException exception = await Assert.ThrowsAsync<ApplicationException>(act);
            Assert.Equal("User with this email is already taken", exception.Message);
        }

        [Fact]
        public async void Handle_Should_Proceed_When_Correct_ConversationId_Provided()
        {
            RegisterUserCommand command = new RegisterUserCommand() { Email = "new@email.pl", Password = "pass", Name = "User1" };

            RegisterUserCommandHandler handler = new RegisterUserCommandHandler(_dbContext.Object, _mapper.Object);

            var result = await handler.Handle(command, default);

            _dbContext.Verify(x => x.Users.AddAsync(It.IsAny<User>(), default), Times.Once());
            _dbContext.Verify(x => x.SaveChangesAsync(default), Times.Once());
        }
    }
}
