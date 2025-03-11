using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Repositories
{
    public interface IObject2DRepository
    {
        Task<IEnumerable<Object2d>> GetAllObject2DsAsync();
        Task<Object2d?> GetObject2DByIdAsync(string id);
        Task AddObject2DAsync(Object2d object2D);
        Task UpdateObject2DAsync(Object2d object2D);
        Task DeleteObject2DAsync(string id);
    }
}