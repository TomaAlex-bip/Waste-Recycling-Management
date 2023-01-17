using WasteRecyclingManagementApi.Core.Repositories;

namespace WasteRecyclingManagementApi.Core
{
    /// <summary>
    /// Repositories container.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UsersRepository { get; }
        
        IRecyclingPointRepository RecyclingPointsRepository { get; }

        IContainerRepository ContainerRepository { get; }

        /// <summary>
        /// Save the changes made to the database.
        /// </summary>
        /// <returns></returns>
        Task<int> CommitAsync();
    }
}
