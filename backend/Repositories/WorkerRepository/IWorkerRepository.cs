using backend.Models;

namespace backend.Repositories
{
    public interface IWorkerRepository
    {
        Task<IEnumerable<Worker>> GetAllAsync(WorkerType? type, bool? available);
        Task<Worker> GetByIdAsync(int id);
        Task AddAsync(Worker worker);
        Task UpdateAsync(Worker worker);
        Task DeleteAsync(Worker worker);
    }
}