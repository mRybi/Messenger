using FluentValidation;
using Messenger.App.Commands;

namespace Messenger.App.Validators
{
    public class RemoveUserFromConversationCommandValidator : AbstractValidator<RemoveUserFromConversationCommand>
    {
        public RemoveUserFromConversationCommandValidator()
        {
            RuleFor(x => x.ConversationId).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
