using FluentValidation;
using Reminder.Models;

namespace Reminder.Validators
{
    /// <summary>
    /// User validator
    /// </summary>
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(SDK.Base.Properties.Resource.NotEmpty);
            RuleFor(x => x.Name).MinimumLength(2).WithMessage(SDK.Base.Properties.Resource.MinimumLength);
        }
    }
}
