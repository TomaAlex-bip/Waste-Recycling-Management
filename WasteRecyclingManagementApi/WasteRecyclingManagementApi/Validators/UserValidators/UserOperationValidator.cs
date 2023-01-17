using FluentValidation;
using WasteRecyclingManagementApi.Core.Configuration;
using WasteRecyclingManagementApi.ViewModels;

namespace WasteRecyclingManagementApi.Validators
{
    public class UserOperationValidator: AbstractValidator<UserOperationViewModel>
    {
        public UserOperationValidator()
        {
            RuleFor(o => o.WasteAmount)
                .NotEmpty()
                .InclusiveBetween(EntityHelperConstants.CONTAINER_MIN_SIZE,
                                  EntityHelperConstants.CONTAINER_MAX_SIZE);

            RuleFor(o => o.ContainerWasteType)
                .NotEmpty()
                .Length(EntityHelperConstants.WASTE_TYPE_MIN_LENGTH,
                        EntityHelperConstants.WASTE_TYPE_MAX_LENGTH);

            RuleFor(o => o.RecyclingPointName)
                .NotEmpty()
                .Length(EntityHelperConstants.RECYCLING_POINT_NAME_MIN_LENGTH,
                        EntityHelperConstants.RECYCLING_POINT_NAME_MAX_LENGTH);
        }
    }
}
