using JetBrains.Annotations;
using WasteRecyclingManagementApi.Core.Entities;

namespace WasteRecyclingManagementApi.Core.Repositories
{
    /// <summary>
    /// Repository for Container entities.
    /// </summary>
    public interface IContainerRepository : IRepository<Container>
    {
        /// <summary>
        /// Gets all the containers in the database.
        /// </summary>
        /// <returns></returns>
        [ItemNotNull]
        Task<IEnumerable<Container>> GetContainersAsync();

        /// <summary>
        /// Gets the container with the specified id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ItemCanBeNull]
        Task<Container?> GetContainerAsync([NotNull] int id);
        
        /// <summary>
        /// Gets the container of specified type in specified recycling point
        /// </summary>
        /// <param name="recyclePointName"></param>
        /// <param name="containerWasteType"></param>
        /// <returns></returns>
        [ItemCanBeNull]
        Task<Container?> GetContainerAsync([NotNull] string recyclePointName,
                                           [NotNull] string containerWasteType);
    }
}
