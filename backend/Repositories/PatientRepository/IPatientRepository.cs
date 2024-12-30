using backend.Models;

namespace backend.Repositories
{
    public interface IPatientRepository
    {
        Task<IEnumerable<Patient>> GetAllAsync();
        Task<Patient> GetByIdAsync(int id);
        Task<Patient> AddAsync(Patient patient);
        Task<bool> UpdateAsync(Patient patient);
        Task DeleteAsync(Patient patient);
    }
}