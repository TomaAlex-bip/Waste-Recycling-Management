using JetBrains.Annotations;
using WasteRecyclingManagementApi.Core.Entities;

namespace WasteRecyclingManagementApi.Core.Repositories
{
    /// <summary>
    /// Repository for RecyclingPoint entities.
    /// </summary>
    public interface IRecyclingPointRepository : IRepository<RecyclingPoint>
    {
        /// <summary>
        /// Gets the recycling point with the specified name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [ItemCanBeNull]
        Task<RecyclingPoint?> GetRecyclingPointAsync([NotNull] string name);

        /// <summary>
        /// Gets the recycling point with the specified location.
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns></returns>
        [ItemCanBeNull]
        Task<RecyclingPoint?> GetRecyclingPointAsync([NotNull] double latitude,
                                                     [NotNull] double longitude);

        /// <summary>
        /// Gets the recycling point with the specified id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ItemCanBeNull]
        Task<RecyclingPoint?> GetRecyclingPointAsync([NotNull] int id);

        /// <summary>
        /// Gets the recycling point with the specified container.
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        [ItemCanBeNull]
        Task<RecyclingPoint?> GetRecyclingPointAsync([NotNull] Container container);

        /// <summary>
        /// Gets all the recycling points
        /// </summary>
        /// <returns></returns>
        [ItemNotNull] 
        Task<IEnumerable<RecyclingPoint>> GetRecyclingPointsAsync();
    }
}
