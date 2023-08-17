using AutoMapper;
using MediatR;
using Messenger.App.Authorization;
using Messenger.App.Commands;
using Messenger.App.Dtos;
using Messenger.App.Handlers;
using Messenger.App.Responses;
using Messenger.Persistence.EF;
using Messenger.Persistence.EF.Models;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Tests.UnitTests.Handlers
{
    public class AuthenticateUserHandlerTests
    {
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<AppDBContext> _dbContext;
        private readonly Mock<IJwtUtils> _jwtUtils;

        public AuthenticateUserHandlerTests()
        {
            _dbContext = new Mock<AppDBContext>();
            _mapper = new Mock<IMapper>();
            _jwtUtils = new Mock<IJwtUtils>();

            _mapper.Setup(x => x.Map<User, AuthenticateUserResponse>(It.IsAny<User>())).Returns(new AuthenticateUserResponse());
            _dbContext.Setup(x => x.Users).ReturnsDbSet(DataHelpers.getTestUsers());


            //var stubUser = new User();
            //var stubUserDto = new AuthenticateUserResponse();
            //var mockMapper = new Mock<IMapper>();
            //_mapper.Setup(mock => mock.Map<User>(It.IsAny<AuthenticateUserResponse>())).Returns(stubUser);
        }

        [Fact]
        public async void Handle_Should_Return_Exception_When_User_With_Given_Email_Do_Not_Exists()
        {
            AuthenticateUserCommand command = new AuthenticateUserCommand() { Email = "test@email.pl", Password = "testPAss" };

            AuthenticateUserCommandHandler handler = new AuthenticateUserCommandHandler(_dbContext.Object, _mapper.Object, _jwtUtils.Object);

            var act = async () => await handler.Handle(command, default);
            Exception exception = await Assert.ThrowsAsync<Exception>(act);
            Assert.Equal("Username or password is incorrect", exception.Message);
        }

        [Fact]
        public async void Handle_Should_Return_Exception_When_Hashed_Password_Is_Incorrect()
        {
            AuthenticateUserCommand command = new AuthenticateUserCommand() { Email = "user2@email.pl", Password = "testPAss" };

            AuthenticateUserCommandHandler handler = new AuthenticateUserCommandHandler(_dbContext.Object, _mapper.Object, _jwtUtils.Object);

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

            AuthenticateUserCommandHandler handler = new AuthenticateUserCommandHandler(_dbContext.Object, _mapper.Object, _jwtUtils.Object);

            var result = await handler.Handle(command, default);

            Assert.NotNull(result);
            Assert.Equal(correctEmail, result.Email);
            //jak zmockowac Automappera???
        }
    }
}
