using Microsoft.EntityFrameworkCore;

public class WorkerRepository : IWorkerRepository
{
    private readonly MyDbContext _context;

    public WorkerRepository(MyDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Worker>> GetAllAsync(WorkerType? type, bool? available)
    {
    var query = _context.Workers
        .Where(w => w.Type == type)
        .Include(w => w.PatientAsDoctor)
        .Include(w => w.PatientAsNurse);

    if (available == true)
    {
        return await query.Where(w => w.PatientAsDoctor == null && w.PatientAsNurse == null).ToListAsync();
    }

    if (available == false)
    {
        return await query.Where(w => w.PatientAsDoctor != null || w.PatientAsNurse != null).ToListAsync();
    }

    return await query.ToListAsync();
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
