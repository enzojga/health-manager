using backend.Models;

namespace backend.Repositories
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> GetAllAsync();
        Task<Room> GetByIdAsync(int id);
        Task<Room> AddAsync(Room room);
        Task UpdateAsync(Room room);
        Task DeleteAsync(Room room);
    }
}