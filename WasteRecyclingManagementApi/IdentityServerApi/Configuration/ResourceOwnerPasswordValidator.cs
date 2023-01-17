using IdentityServer4.Models;
using IdentityServer4.Validation;
using WasteRecyclingManagementApi.Core.Repositories;

namespace IdentityServerApi.Configuration
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IUserRepository _userRepository;

        public ResourceOwnerPasswordValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var user = await _userRepository.GetUserWithCredentialsAsync(context.UserName, context.Password);

            if(user == null)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid Credentials!");
            }
            else
            {
                context.Result = new GrantValidationResult(user.Id.ToString(), authenticationMethod: "custom");
            }
        }
    }
}
