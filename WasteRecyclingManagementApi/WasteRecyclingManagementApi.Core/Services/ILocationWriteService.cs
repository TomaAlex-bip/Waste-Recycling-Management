using JetBrains.Annotations;
using WasteRecyclingManagementApi.Core.Dtos;

namespace WasteRecyclingManagementApi.Core.Services
{
    /// <summary>
    /// Service for writing new locations(recycling point and containers).
    /// </summary>
    public interface ILocationWriteService
    {
        /// <summary>
        /// Adds a new recycling point.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns>An RecyclingPointDto which has a field of type ErrorMessageDto which can be null 
        /// if everything went well, or contains the encountered error message.</returns>
        [ItemNotNull]
        Task<RecyclingPointDto> AddRecyclingPointAsync([NotNull] string name, 
                                                       [NotNull] double latitude, 
                                                       [NotNull] double longitude);

        /// <summary>
        /// Adds new containers to a specified recycling point.
        /// </summary>
        /// <param name="recyclingPointId"></param>
        /// <param name="containerDtos"></param>
        /// <returns>An ErrorMessageDto with an errorMessage if there are 
        /// any errors while performing the action, or null, if everything went well.</returns>
        [ItemCanBeNull]
        Task<ErrorMessageResponse?> AddContainersAsync([NotNull] int recyclingPointId,
                                                  [NotNull] IEnumerable<ContainerDto> containerDtos);
    }
}
