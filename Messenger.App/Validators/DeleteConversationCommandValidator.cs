using FluentValidation;
using Messenger.App.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.App.Validators
{
    public class DeleteConversationCommandValidator : AbstractValidator<DeleteConversationCommand>
    {
        public DeleteConversationCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
