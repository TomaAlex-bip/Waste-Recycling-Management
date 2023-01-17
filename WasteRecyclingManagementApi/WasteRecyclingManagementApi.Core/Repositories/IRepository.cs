using JetBrains.Annotations;

namespace WasteRecyclingManagementApi.Core.Repositories
{
    /// <summary>
    /// Generalized Repository for TEntities.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Gets all the TEntities in the database.
        /// </summary>
        /// <returns></returns>
        [ItemNotNull]
        Task<IEnumerable<TEntity>> GetAsync();

        /// <summary>
        /// Adds a new TEntity to database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [ItemNotNull]
        Task AddAsync([NotNull] TEntity entity);

        /// <summary>
        /// Updates a TEntity form the database.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [ItemNotNull]
        TEntity Update([NotNull] TEntity entity);
        
        /// <summary>
        /// Removes a TEntity from the database.
        /// </summary>
        /// <param name="entity"></param>
        [ItemNotNull]
        void Remove([NotNull] TEntity entity);
    }
}
