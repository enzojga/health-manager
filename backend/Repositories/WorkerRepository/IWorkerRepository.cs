public interface IWorkerRepository
{
    Task<IEnumerable<Worker>> GetAllAsync();
    Task<Worker> GetByIdAsync(int id);
    Task AddAsync(Worker worker);
    Task UpdateAsync(Worker worker);
    Task DeleteAsync(Worker worker);
}
