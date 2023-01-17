using FluentValidation;
using WasteRecyclingManagementApi.Core.Configuration;
using WasteRecyclingManagementApi.ViewModels;

namespace WasteRecyclingManagementApi.Validators
{
    public class UserValidator: AbstractValidator<UserViewModel>
    {
        public UserValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .Length(EntityHelperConstants.USERNAME_MIN_LENGTH,
                        EntityHelperConstants.USERNAME_MAX_LENGTH);

            RuleFor(x => x.Password)
                .NotEmpty()
                .Length(EntityHelperConstants.PASSWORD_MIN_LENGTH,
                        EntityHelperConstants.PASSWORD_MAX_LENGTH);
        }
    }
}
