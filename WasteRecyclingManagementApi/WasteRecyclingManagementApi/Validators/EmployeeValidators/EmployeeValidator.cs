using FluentValidation;
using WasteRecyclingManagementApi.Core.Configuration;
using WasteRecyclingManagementApi.ViewModels;

namespace WasteRecyclingManagementApi.Validators
{
    public class EmployeeValidator: AbstractValidator<EmployeeViewModel>
    {
        public EmployeeValidator()
        {
            RuleFor(e => e.Username)
                .NotEmpty()
                .Length(EntityHelperConstants.USERNAME_MIN_LENGTH,
                        EntityHelperConstants.USERNAME_MAX_LENGTH);

            RuleFor(e => e.Role)
                .NotNull()
                .InclusiveBetween(EntityHelperConstants.USER_MIN_ROLE,
                                  EntityHelperConstants.USER_MAX_ROLE);
        }
    }
}
