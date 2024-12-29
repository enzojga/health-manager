using Microsoft.EntityFrameworkCore;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly MyDbContext _context;

    public AppointmentRepository(MyDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Appointment>> GetAllAsync()
    {
        return await _context.Appointments
            .Include(a => a.Patient)
            .ToListAsync();
    }

    public async Task<Appointment> GetByIdAsync(int id)
    {
        return await _context.Appointments.FindAsync(id);
    }

    public async Task<Appointment> GetLastByPatiantId(int id)
    {
        return await _context.Appointments
            .Include(a => a.Patient)
            .Where(a => a.UserId == id)
            .OrderByDescending(a => a.CreatedAt)
            .FirstOrDefaultAsync();
    }

    public async Task<Appointment> AddAsync(Appointment appointment)
    {
        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();
        return appointment;
    }

    public async Task<bool> UpdateAsync(Appointment appointment)
    {
        _context.Appointments.Update(appointment);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task DeleteAsync(Appointment appointment)
    {
        _context.Appointments.Remove(appointment);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Appointment>> GetByPatientIdAsync(int userId)
    {
        return await _context.Appointments
            .Where(a => a.UserId == userId)
            .Include(a => a.Patient)
            .ToListAsync();
    }

    public async Task<IEnumerable<Appointment>> GetNotFinishedAppointmentsAsync()
    {
        return await _context.Appointments
            .Where(a => !a.Finished)
            .Include(a => a.Patient)
            .ToListAsync();
    }
}