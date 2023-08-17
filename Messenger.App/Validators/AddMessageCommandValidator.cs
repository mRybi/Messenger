using FluentValidation;
using Messenger.App.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.App.Validators
{
    public class AddMessageCommandValidator : AbstractValidator<AddMessageCommand>
    {
        public AddMessageCommandValidator()
        {
            RuleFor(x => x.SenderId).NotEmpty();
            RuleFor(x => x.ConversationId).NotEmpty();
            RuleFor(x => x.Message).NotEmpty();
        }
    }
}
