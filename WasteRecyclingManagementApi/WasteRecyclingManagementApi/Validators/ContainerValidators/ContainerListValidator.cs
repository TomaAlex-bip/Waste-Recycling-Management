using FluentValidation;
using WasteRecyclingManagementApi.ViewModels;

namespace WasteRecyclingManagementApi.Validators
{
    public class ContainerListValidator: AbstractValidator<ContainerViewModel[]>
    {
        public ContainerListValidator()
        {
            RuleForEach(c => c).SetValidator(new ContainerValidator());
        }
    }
}
