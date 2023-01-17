using FluentValidation;
using WasteRecyclingManagementApi.Core.Configuration;
using WasteRecyclingManagementApi.ViewModels;

namespace WasteRecyclingManagementApi.Validators
{
    public class RecyclingPointValidator: AbstractValidator<RecyclingPointViewModel>
    {
        public RecyclingPointValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .Length(EntityHelperConstants.RECYCLING_POINT_NAME_MIN_LENGTH,
                        EntityHelperConstants.RECYCLING_POINT_NAME_MAX_LENGTH);

            RuleFor(c => c.Latitude)
                .NotEmpty()
                .InclusiveBetween(EntityHelperConstants.MIN_LATITUDE,
                                  EntityHelperConstants.MAX_LATITUDE);

            RuleFor(c => c.Longitude)
                .NotEmpty()
                .InclusiveBetween(EntityHelperConstants.MIN_LONGITUDE,
                                  EntityHelperConstants.MAX_LONGITUDE);
        }
    }
}
