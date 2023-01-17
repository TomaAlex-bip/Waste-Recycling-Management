using JetBrains.Annotations;
using WasteRecyclingManagementApi.Core.Entities;

namespace WasteRecyclingManagementApi.Core.Repositories
{
    /// <summary>
    /// Repository for User entities.
    /// </summary>
    public interface IUserRepository : IRepository<User>
    {
        /// <summary>
        /// Gets the user with specified credentials.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="hash"></param>
        /// <returns>The found user, or null if there where any errors.</returns>
        [ItemCanBeNull]
        Task<User?> GetUserWithCredentialsAsync([NotNull] string username,
                                                [NotNull] string hash);

        /// <summary>
        /// Creates a new user with specified credentials
        /// </summary>
        /// <param name="username"></param>
        /// <param name="hash"></param>
        /// <returns>The newly created user.</returns>
        [ItemNotNull]
        Task<User> RegisterUserAsync([NotNull] string username,
                                     [NotNull] string hash);

        /// <summary>
        /// Gets the user with specified id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ItemCanBeNull]
        Task<User?> GetUserAsync([NotNull] int id);

        /// <summary>
        /// Gets the user with specified username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [ItemCanBeNull]
        Task<User?> GetUserAsync([NotNull] string username);
    }
}
