using MediatR;
using Messenger.Api.Controllers;
using Messenger.App.Commands;
using Messenger.App.Queries;
using Messenger.Persistence.EF.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Tests.UnitTests.Controllers
{
    public class ConversationControllerTests
    {
        private Mock<IMediator> _mediator;
        private ConversationController _controller;

        public ConversationControllerTests()
        {
            _mediator = new Mock<IMediator>();
            _controller = new ConversationController(_mediator.Object);
        }
        
        [Fact]
        public async Task CreateConversation_ShouldReturnCreatedWithConversationId()
        {
            var user1Id = Guid.NewGuid();
            var user2Id = Guid.NewGuid();

            var command = new CreateConversationCommand { Name = "convo", IsGroup = false, UserIds = new List<Guid>() { user1Id, user2Id } };

            var response = await _controller.Post(command) as CreatedResult;

            Assert.NotNull(response);
            Assert.IsType<CreatedResult>(response);
            Assert.IsType<Guid>(response.Value);
        }

        [Fact]
        public async Task AddUserToConversation_ShouldReturnOk()
        {
            var command = new AddUserToConversationCommand { ConversationId = Guid.NewGuid(), UserIds = new List<Guid>() { Guid.NewGuid() } };

            var response = await _controller.AddUser(command) as OkResult;

            Assert.NotNull(response);
            Assert.IsType<OkResult>(response);
        }

        [Fact]
        public async Task RemoveUserFromConversation_ShouldReturnOk()
        {
            var command = new RemoveUserFromConversationCommand { ConversationId = Guid.NewGuid(), UserId = Guid.NewGuid() };

            var response = await _controller.RemoveUser(command) as OkResult;

            Assert.NotNull(response);
            Assert.IsType<OkResult>(response);
        }

        [Fact]
        public async Task DeleteConversation_ShouldReturnOk()
        {
            var command = new DeleteConversationCommand { Id = Guid.NewGuid() };

            var response = await _controller.DeleteConversation(command) as OkResult;

            Assert.NotNull(response);
            Assert.IsType<OkResult>(response);
        }
    }
}
