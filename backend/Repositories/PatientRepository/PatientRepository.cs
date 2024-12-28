using Microsoft.EntityFrameworkCore;

public class PatientRepository : IPatientRepository
{
    private readonly MyDbContext _context;

    public PatientRepository(MyDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Patient>> GetAllAsync()
    {
        return await _context.Patients
        .Include(p => p.Doctor)
        .Include(p => p.Nurse)
        .ToListAsync();
    }

    public async Task<Patient> GetByIdAsync(int id)
    {
        return await _context.Patients.FindAsync(id);
    }

    public async Task<Patient> AddAsync(Patient patient)
    {
        _context.Patients.Add(patient);
        await _context.SaveChangesAsync();
        return patient;
    }

    public async Task<bool> UpdateAsync(Patient patient)
    {
        _context.Patients.Update(patient);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var patient = await _context.Patients.FindAsync(id);
        if (patient == null)
        {
            return false;
        }

        _context.Patients.Remove(patient);
        return await _context.SaveChangesAsync() > 0;
    }
}