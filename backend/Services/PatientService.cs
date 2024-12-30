public class PatientService
{
    private readonly IPatientRepository _patientRepository;
    private readonly WorkerService _workerService;
    private readonly AppointmentService _appointmentService;
    private readonly RoomService _roomService;

    public PatientService(
        IPatientRepository patientRepository,
        WorkerService workerService,
        AppointmentService appointmentService,
        RoomService roomService
    )
    {
        _patientRepository = patientRepository;
        _workerService = workerService;
        _appointmentService = appointmentService;
        _roomService = roomService;
    }

    public async Task<IEnumerable<Patient>> GetAllPatientsAsync()
    {
        var patients = await _patientRepository.GetAllAsync();
        return patients;
    }

    public async Task<Patient> GetPatientByIdAsync(int id)
    {
        var patient = await _patientRepository.GetByIdAsync(id);
        if (patient == null)
        {
            return null;
        }

        return patient;
    }

    public async Task<Patient> CreatePatientAsync(PatientDto patientDto)
    {
        var patient = new Patient
        {
            Name = patientDto.Name,
            Cpf = patientDto.Cpf,
        };

        return await _patientRepository.AddAsync(patient);
    }

    public async Task<bool> UpdatePatientAsync(int id, PatientDto patientDto)
    {
        var patient = await _patientRepository.GetByIdAsync(id);
        if (patient == null)
        {
            return false;
        }

        patient.Name = patientDto.Name;
        patient.Cpf = patientDto.Cpf;
        patient.UpdatedAt = DateTime.Now;

        return await _patientRepository.UpdateAsync(patient);
    }

    public async Task<bool> DeletePatientAsync(int id)
    {
        var patient = await _patientRepository.GetByIdAsync(id);
        if (patient == null)
        {
            return false;
        }

        await _patientRepository.DeleteAsync(patient);
        return true;
    }

    public async Task<Patient> AssociateNurseToPatientAsync(int patientId, int nurseId)
    {
        await GetLastAppointmentOrThrowException(patientId);
        var patient = await GetPatientByIdAsync(patientId);
        var patientAlreadyBeenAssigned = patient.NurseId != null || patient.DoctorId != null || patient.RoomId != null;
        if (patientAlreadyBeenAssigned)
        {
            throw new Exception("Patient not found or already been assigned");
        }

        var nurse = await _workerService.GetWorkerByIdAsync(nurseId);
        if (nurse == null || nurse.Type != WorkerType.Nurse)
        {
            throw new Exception("Worker not found or is not a nurse");
        }

        patient.NurseId = nurse.Id;
        await _patientRepository.UpdateAsync(patient);
        return patient;
    }

    public async Task<Patient> AssociateDoctorToPatientAsync(int patientId, int nurseId)
    {
        await GetLastAppointmentOrThrowException(patientId);
        var patient = await GetPatientByIdAsync(patientId);
        var patientAlreadyBeenAssigned = patient == null || patient.NurseId == null || patient.DoctorId != null || patient.RoomId != null;
        if (patientAlreadyBeenAssigned)
        {
            throw new Exception("Patient not found or already been assigned");
        }

        var nurse = await _workerService.GetWorkerByIdAsync(nurseId);
        if (nurse == null || nurse.Type != WorkerType.Doctor)
        {
            throw new Exception("Worker not found or is not a nurse");
        }

        patient.NurseId = null;
        patient.DoctorId = nurse.Id;
        await _patientRepository.UpdateAsync(patient);
        return patient;
    }

    public async Task<Patient> AssociateRoomToPatientAsync(int patientId, int roomId)
    {
        await GetLastAppointmentOrThrowException(patientId);

        var patient = await GetPatientByIdAsync(patientId);
        var patientAlreadyBeenAssigned = patient == null || patient.NurseId != null || patient.DoctorId == null;
        if (patientAlreadyBeenAssigned)
        {
            throw new Exception("Patient not found or already been assigned");
        }

        var room = await _roomService.GetRoomByIdAsync(roomId);
        var roomOutOfCapacity = room == null || room.Patients != null && (room.Patients.Count == room.Capacity) || room.Patients == null;
        if(roomOutOfCapacity)
        {
            throw new Exception("Room not found or is not free");
        }

        patient.NurseId = null;
        patient.DoctorId = null;
        patient.RoomId = roomId;

        await _patientRepository.UpdateAsync(patient);
        return patient;
    }

    public async Task<Patient> FinishPatientAppointmentAsync(int patientId)
    {
        var appointment = await GetLastAppointmentOrThrowException(patientId);
        
        var patient = await GetPatientByIdAsync(patientId);
        var patientAlreadyBeenAssigned = patient == null || patient.NurseId != null || (patient.DoctorId == null && patient.RoomId == null);
        if (patientAlreadyBeenAssigned)
        {
            throw new Exception("Patient not found or already been assigned");
        }

        patient.NurseId = null;
        patient.DoctorId = null;
        patient.RoomId = null;

        await _appointmentService.FinishAppointmentAsync(appointment.Id);
        await _patientRepository.UpdateAsync(patient);

        return patient;
    }

    private async Task<Appointment> GetLastAppointmentOrThrowException(int patientId)
    {
        var appointment = await _appointmentService.GetLastAppointmentByPatientAsync(patientId);
        if (appointment == null || appointment.Finished)
        {
            throw new Exception("No appointment found for the user");
        }
        return appointment;
    }
}