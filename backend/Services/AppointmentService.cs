public class AppointmentService
{
    private readonly IAppointmentRepository _appointmentRepository;

    public AppointmentService(IAppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
    }

    public async Task<Appointment> AddAppointmentAsync(AppointmentDto appointmentDto)
    {
        var appointment = new Appointment
        {
            UserId = appointmentDto.UserId,
        };

        return await _appointmentRepository.AddAsync(appointment);
    }

    public async Task<bool> UpdateAppointmentAsync(int id, AppointmentDto appointmentDto)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(id);
        if (appointment == null)
        {
            return false;
        }

        return await _appointmentRepository.UpdateAsync(appointment);
    }

    public async Task<bool> DeleteAppointmentAsync(int id)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(id);
        if (appointment == null)
        {
            return false;
        }
        await _appointmentRepository.DeleteAsync(appointment);
        return true;
    }

    public async Task<IEnumerable<Appointment>> GetAppointmentsByPatientAsync(int userId)
    {
        return await _appointmentRepository.GetByPatientIdAsync(userId);
    }

}