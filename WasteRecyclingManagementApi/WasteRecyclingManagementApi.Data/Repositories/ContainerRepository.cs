using Microsoft.EntityFrameworkCore;
using WasteRecyclingManagementApi.Core.Entities;
using WasteRecyclingManagementApi.Core.Repositories;

namespace WasteRecyclingManagementApi.Data.Repositories
{
    public class ContainerRepository : Repository<Container>, IContainerRepository
    {
        public ContainerRepository(RecyclingDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Container?> GetContainerAsync(int id)
        {
            return await _dbContext.Containers
                .Include(c => c.Operations)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Container?> GetContainerAsync(string recyclePointName, string containerWasteType)
        {
            return await _dbContext.Containers
                .Include(c => c.Operations)
                .Include(c => c.RecyclingPoint)
                .FirstOrDefaultAsync(c => c.RecyclingPoint.Name == recyclePointName && 
                                          c.Type == containerWasteType);
        }

        public async Task<IEnumerable<Container>> GetContainersAsync()
        {
            return await _dbContext.Containers
                .Include(c => c.Operations)
                .Include(c => c.RecyclingPoint)
                .ToListAsync();
        }
    }
}
