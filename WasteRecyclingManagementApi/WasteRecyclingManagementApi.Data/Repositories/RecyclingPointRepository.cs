using Microsoft.EntityFrameworkCore;
using WasteRecyclingManagementApi.Core.Entities;
using WasteRecyclingManagementApi.Core.Repositories;

namespace WasteRecyclingManagementApi.Data.Repositories
{
    public class RecyclingPointRepository : Repository<RecyclingPoint>, IRecyclingPointRepository
    {
        public RecyclingPointRepository(RecyclingDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<RecyclingPoint?> GetRecyclingPointAsync(double latitude, double longitude)
        {
            return await _dbContext.RecyclingPoints
                .Include(r => r.Containers)
                .FirstOrDefaultAsync(r => r.Latitude == latitude && 
                                          r.Longitude == longitude);
        }

        public async Task<RecyclingPoint?> GetRecyclingPointAsync(string name)
        {
            return await _dbContext.RecyclingPoints
                .Include(r => r.Containers)
                .FirstOrDefaultAsync(r => r.Name == name);
        }

        public async Task<RecyclingPoint?> GetRecyclingPointAsync(int id)
        {
            return await _dbContext.RecyclingPoints
                .Include(r => r.Containers)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<RecyclingPoint?> GetRecyclingPointAsync(Container container)
        {
            return await _dbContext.RecyclingPoints
                .Include(r => r.Containers)
                .FirstOrDefaultAsync(r => r.Containers.Contains(container));
        }

        public async Task<IEnumerable<RecyclingPoint>> GetRecyclingPointsAsync()
        {
            return await _dbContext.RecyclingPoints
                .Include(r => r.Containers)
                .ToListAsync();
        }
    }
}
