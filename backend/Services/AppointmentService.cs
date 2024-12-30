using backend.DTOS;
using backend.Models;
using backend.Repositories;

namespace backend.Services{
    public class AppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<Appointment> AddAppointmentAsync(AppointmentDto appointmentDto)
        {
            var appointmentStarted = await _appointmentRepository.GetLastByPatiantId(appointmentDto.UserId);

            if(appointmentStarted != null && !appointmentStarted.Finished)
            {
                return null;
            }
            
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

        public async Task<IEnumerable<Appointment>> GetAppointmentByPatientAsync(int userId)
        {
            return await _appointmentRepository.GetByPatientIdAsync(userId);
        }

        public async Task<Appointment> FinishAppointmentAsync(int id)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(id);
            if (appointment == null)
            {
                return null;
            }

            appointment.Finished = true;
            appointment.UpdatedAt = DateTime.Now;

            await _appointmentRepository.UpdateAsync(appointment);
            return appointment;
        }

        public async Task<Appointment> GetLastAppointmentByPatientAsync(int userId)
        {
            return await _appointmentRepository.GetLastByPatiantId(userId);
        }

        public async Task<IEnumerable<Appointment>> GetNotFinishedAppointmentsAsync()
        {
            return await _appointmentRepository.GetNotFinishedAppointmentsAsync();
        }
        
    }
}
