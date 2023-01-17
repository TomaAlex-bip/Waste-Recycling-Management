using WasteRecyclingManagementApi.Core;
using WasteRecyclingManagementApi.Core.Repositories;
using WasteRecyclingManagementApi.Data.Repositories;

namespace WasteRecyclingManagementApi.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RecyclingDbContext _dbContext;

        public UnitOfWork(RecyclingDbContext dbContext)
        {
            _dbContext = dbContext;

            UsersRepository = new UserRepository(_dbContext);
            RecyclingPointsRepository = new RecyclingPointRepository(_dbContext);
            ContainerRepository = new ContainerRepository(_dbContext);
        }

        public IUserRepository UsersRepository { get; private set; }

        public IRecyclingPointRepository RecyclingPointsRepository { get; private set; }

        public IContainerRepository ContainerRepository { get; private set; }

        public async Task<int> CommitAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
