using JetBrains.Annotations;
using WasteRecyclingManagementApi.Core.Dtos;

namespace WasteRecyclingManagementApi.Core.Services
{
    /// <summary>
    /// Service for admin operations.
    /// </summary>
    public interface IAdminService
    {
        /// <summary>
        /// Gets all the operations done by any user in any container.
        /// </summary>
        /// <returns>A collection of OperationDtos.</returns>
        [ItemNotNull]
        Task<IEnumerable<OperationDto>> GetAllOperationsAsync();

        /// <summary>
        /// Changes the role of an user, either an employee, or back to a normal user.
        /// </summary>
        /// <param name="username">The username of the user</param>
        /// <param name="role">The new role of the user (0 for normal user, and 1 for employee)</param>
        /// <returns>An ErrorMessageDto with an errorMessage if there are 
        /// any errors while performing the action, or null, if everything went well.</returns>
        [ItemCanBeNull]
        Task<ErrorMessageResponse?> ChangeUserRoleAsync([NotNull] string username, 
                                                   [NotNull] int role);

        /// <summary>
        /// Removes a recycling point form the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>An ErrorMessageDto with an errorMessage if there are 
        /// any errors while performing the action, or null, if everything went well.</returns>
        [ItemCanBeNull]
        Task<ErrorMessageResponse?> RemoveRecyclingPoint([NotNull] int id);
    }
}
