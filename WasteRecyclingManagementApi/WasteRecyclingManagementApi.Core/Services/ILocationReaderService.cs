using JetBrains.Annotations;
using WasteRecyclingManagementApi.Core.Dtos;

namespace WasteRecyclingManagementApi.Core.Services
{
    /// <summary>
    /// Service for reading locations (recycling points and containers).
    /// </summary>
    public interface ILocationReaderService
    {
        /// <summary>
        /// Gets all the available recycling points.
        /// </summary>
        /// <returns>A collection of RecyclingPointDtos which contains only the public information (name, latitude, longitude)</returns>
        [ItemNotNull]
        Task<IEnumerable<RecyclingPointDto>> GetPublicRecyclingPointsAsync();

        /// <summary>
        /// Gets all the existing Recycling Points.
        /// </summary>
        /// <returns>A colection of RecyclingPointDtos.</returns>
        [ItemNotNull]
        Task<IEnumerable<RecyclingPointDto>> GetRecyclingPointsAsync();

        /// <summary>
        /// Gets the recycling point with the specified id.
        /// </summary>
        /// <param name="id">The id of the recyling point</param>
        /// <returns>An RecyclingPointDto which has a field of type ErrorMessageDto which can be null 
        /// if everything went well, or contains the encountered error message.</returns>
        [ItemNotNull]
        Task<RecyclingPointDto> GetRecyclingPointAsync([NotNull] int id);

        /// <summary>
        /// Gets all the existing containers.
        /// </summary>
        /// <returns>A colection of ContainerDto </returns>
        [ItemNotNull]
        Task<IEnumerable<ContainerDto>> GetContainersAsync();

        /// <summary>
        /// Gets the container with the specified id.
        /// </summary>
        /// <param name="id">The id of the container</param>
        /// <returns>An ContainerWithErrorDto which has a field of type ErrorMessageDto which can be null 
        /// if everything went well, or contains the encountered error message.</returns>
        [ItemNotNull]
        Task<ContainerWithErrorDto> GetContainerAsync([NotNull] int id);
    }
}
