using AutoMapper;
using MediatR;
using Messenger.App;
using Messenger.App.Authorization;
using Messenger.App.Commands;
using Messenger.App.Dtos;
using Messenger.App.Handlers;
using Messenger.App.Responses;
using Messenger.Persistence.EF;
using Messenger.Persistence.EF.Models;
using Moq;
using Moq.EntityFrameworkCore;

namespace Messenger.Tests.UnitTests.Handlers
{
    public class AuthenticateUserHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<AppDBContext> _dbContext;
        private readonly Mock<IJwtUtils> _jwtUtils;

        public AuthenticateUserHandlerTests()
        {
            var myProfile = new AppMappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            var mapper = new Mapper(configuration);

            _mapper = mapper;
            _dbContext = new Mock<AppDBContext>();
            _jwtUtils = new Mock<IJwtUtils>();
            _dbContext.Setup(x => x.Users).ReturnsDbSet(DataHelpers.getTestUsers());

        }

        [Fact]
        public async void Handle_Should_Return_Exception_When_User_With_Given_Email_Do_Not_Exists()
        {
            AuthenticateUserCommand command = new AuthenticateUserCommand() { Email = "test@email.pl", Password = "testPAss" };

            AuthenticateUserCommandHandler handler = new AuthenticateUserCommandHandler(_dbContext.Object, _mapper, _jwtUtils.Object);

            var act = async () => await handler.Handle(command, default);
            Exception exception = await Assert.ThrowsAsync<Exception>(act);
            Assert.Equal("Username or password is incorrect", exception.Message);
        }

        [Fact]
        public async void Handle_Should_Return_Exception_When_Hashed_Password_Is_Incorrect()
        {
            AuthenticateUserCommand command = new AuthenticateUserCommand() { Email = "user2@email.pl", Password = "testPAss" };

            AuthenticateUserCommandHandler handler = new AuthenticateUserCommandHandler(_dbContext.Object, _mapper, _jwtUtils.Object);

            var act = async () => await handler.Handle(command, default);
            Exception exception = await Assert.ThrowsAsync<Exception>(act);
            Assert.Equal("Username or password is incorrect", exception.Message);
        }

        [Fact]
        public async void Handle_Should_Return_Response_When_Email_And_Password_Is_Correct()
        {
            var correctEmail = "user2@email.pl";
            var correctPassword = "12345";

            AuthenticateUserCommand command = new AuthenticateUserCommand() { Email = correctEmail, Password = correctPassword };

            AuthenticateUserCommandHandler handler = new AuthenticateUserCommandHandler(_dbContext.Object, _mapper, _jwtUtils.Object);

            var result = await handler.Handle(command, default);

            Assert.NotNull(result);
            Assert.Equal(correctEmail, result.Email);
        }
    }
}
