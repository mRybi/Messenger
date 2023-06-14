using FluentValidation;
using Messenger.App.Commands;

namespace Messenger.App.Validators
{
    public class CreateConversationCommandValidator : AbstractValidator<CreateConversationCommand>
    {
        public CreateConversationCommandValidator()
        {
            RuleFor(x => x.IsGroup).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.UserIds).NotEmpty();
        }
    }
}
