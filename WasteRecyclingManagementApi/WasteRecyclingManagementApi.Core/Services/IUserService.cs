using JetBrains.Annotations;
using WasteRecyclingManagementApi.Core.Dtos;

namespace WasteRecyclingManagementApi.Core.Services
{
    /// <summary>
    /// Service for user operations.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Gets all the operations done by the specified user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>A colection of OperationDtos which represents the operations of the specified user, or null if the specified user is not valid.</returns>
        [ItemCanBeNull]
        Task<IEnumerable<OperationDto>?> GetUserOperationsAsync([NotNull] int userId);

        /// <summary>
        /// Registers an operation of adding waste to a specified container by a specified user.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="recyclingPointName"></param>
        /// <param name="containerWasteType"></param>
        /// <param name="wasteAmount"></param>
        /// <returns>An UserOperationDto which has a field of type ErrorMessageDto which can be null 
        /// if everything went well, or contains the encountered error message.</returns>
        [ItemNotNull]
        Task<UserOperationDto> MakeAnOperationAsync([NotNull] int userId,
                                                    [NotNull] string recyclingPointName,
                                                    [NotNull] string containerWasteType,
                                                    [NotNull] decimal wasteAmount);
    }
}
