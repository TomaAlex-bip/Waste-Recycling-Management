using FluentValidation;
using WasteRecyclingManagementApi.Core.Configuration;
using WasteRecyclingManagementApi.ViewModels;

namespace WasteRecyclingManagementApi.Validators
{
    public class ContainerValidator: AbstractValidator<ContainerViewModel>
    {
        public ContainerValidator()
        {
            RuleFor(c => c.WasteType)
                .NotEmpty()
                .Length(EntityHelperConstants.WASTE_TYPE_MIN_LENGTH,
                        EntityHelperConstants.WASTE_TYPE_MAX_LENGTH);

            RuleFor(c => c.TotalCapacity)
                .NotEmpty()
                .InclusiveBetween(EntityHelperConstants.CONTAINER_MIN_SIZE,
                                  EntityHelperConstants.CONTAINER_MAX_SIZE);

            RuleFor(c => c.MeasureUnit)
                .NotEmpty()
                .Length(EntityHelperConstants.MEASURE_UNIT_MIN_LENGTH,
                        EntityHelperConstants.MEASURE_UNIT_MAX_LENGTH);
        }
    }
}
