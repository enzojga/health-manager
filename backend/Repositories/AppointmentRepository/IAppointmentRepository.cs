public interface IAppointmentRepository
{
    Task<IEnumerable<Appointment>> GetAllAsync();
    Task<Appointment> GetByIdAsync(int id);
    Task<Appointment> AddAsync(Appointment appointment);
    Task<bool> UpdateAsync(Appointment appointment);
    Task DeleteAsync(Appointment appointment);
    Task<IEnumerable<Appointment>> GetByPatientIdAsync(int userId);
    Task<Appointment> GetLastByPatiantId(int id);
    Task<IEnumerable<Appointment>> GetNotFinishedAppointmentsAsync();
}