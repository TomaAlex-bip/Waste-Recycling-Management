using JetBrains.Annotations;
using WasteRecyclingManagementApi.Core.Dtos;

namespace WasteRecyclingManagementApi.Core.Services
{
    /// <summary>
    /// Service for public registration operation.
    /// </summary>
    public interface IRegistrationService
    {
        /// <summary>
        /// Register an user with specified credentials.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>An RegistrationDto which has a field of type ErrorMessageDto which can be null 
        /// if everything went well, or contains the encountered error message.</returns>
        [ItemNotNull]
        Task<UserRegistrationDto> RegisterUserAsync([NotNull] string username,
                                                    [NotNull] string password);
    }
}
