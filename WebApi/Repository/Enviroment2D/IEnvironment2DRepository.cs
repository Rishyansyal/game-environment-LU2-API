using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Repositories
{
    public interface IEnvironment2DRepository
    {
        Task<IEnumerable<Environment2d>> GetAllEnvironment2DsAsync();
        Task<Environment2d?> GetWorldByIdAsync(int id);
        Task AddWorldAsync(Environment2d environment2D);
        Task UpdateWorldAsync(Environment2d environment2D);
        Task DeleteWorldAsync(int id);
        Task<IEnumerable<Object2d>> GetObjectsForWorld(int worldId);
    }
}