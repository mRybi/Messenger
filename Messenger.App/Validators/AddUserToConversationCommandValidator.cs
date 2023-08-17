using FluentValidation;
using Messenger.App.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.App.Validators
{
    public class AddUserToConversationCommandValidator : AbstractValidator<AddUserToConversationCommand>
    {
        public AddUserToConversationCommandValidator()
        {
            RuleFor(x => x.ConversationId).NotEmpty();
            RuleFor(x => x.UserIds).NotEmpty();
        }
    }
}
