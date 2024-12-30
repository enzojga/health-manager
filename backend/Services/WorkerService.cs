using Microsoft.EntityFrameworkCore;
public class WorkerService
{
    private readonly IWorkerRepository _workerRepository;

    public WorkerService(IWorkerRepository workerRepository)
    {
        _workerRepository = workerRepository;
    }

    public async Task<IEnumerable<Worker>> GetAllWorkersAsync(WorkerType? type, bool? available)
    {
        return await _workerRepository.GetAllAsync(type, available);
    }

    public async Task<Worker> GetWorkerByIdAsync(int id)
    {
        var worker = await _workerRepository.GetByIdAsync(id);
        if (worker == null)
        {
            return null;
        }

        return worker;
    }

    public async Task<Worker> CreateWorkerAsync(WorkerDto workerDto)
    {
        var worker = new Worker
        {
            Name = workerDto.Name,
            Type = workerDto.Type,
        };

        await _workerRepository.AddAsync(worker);
        return worker;
    }

    public async Task<bool> UpdateWorkerAsync(int id, WorkerDto workerDto)
    {
        var worker = await _workerRepository.GetByIdAsync(id);
        if (worker == null)
        {
            return false;
        }

        worker.Name = workerDto.Name;
        worker.Type = workerDto.Type;

        await _workerRepository.UpdateAsync(worker);
        return true;
    }

    public async Task<bool> DeleteWorkerAsync(int id)
    {
        var worker = await _workerRepository.GetByIdAsync(id);
        if (worker == null)
        {
            return false;
        }

        await _workerRepository.DeleteAsync(worker);
        return true;
    }
}