using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public class WorkerRepository : IWorkerRepository
{
    private readonly MyDbContext _context;

    public WorkerRepository(MyDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Worker>> GetAllAsync()
    {
        return await _context.Workers.ToListAsync();
    }

    public async Task<Worker> GetByIdAsync(int id)
    {
        return await _context.Workers.FindAsync(id);
    }

    public async Task AddAsync(Worker worker)
    {
        _context.Workers.Add(worker);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Worker worker)
    {
        _context.Workers.Update(worker);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Worker worker)
    {
        _context.Workers.Remove(worker);
        await _context.SaveChangesAsync();
    }
}
