public class PatientService
{
    private readonly IPatientRepository _patientRepository;
    private readonly WorkerService _workerService;
    private readonly AppointmentService _appointmentService;

    public PatientService(IPatientRepository patientRepository, WorkerService workerService, AppointmentService appointmentService)
    {
        _patientRepository = patientRepository;
        _workerService = workerService;
        _appointmentService = appointmentService;
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
        var patient = await GetPatientByIdAsync(patientId);
        if (patient == null || patient.NurseId != null || patient.DoctorId != null)
        {
            throw new Exception("Patient not found");
        }

        var nurse = await _workerService.GetWorkerByIdAsync(nurseId);
        if (nurse == null || nurse.Type != WorkerType.Nurse)
        {
            throw new Exception("Worker not found or is not a nurse");
        }

        var appointment = await _appointmentService.GetAppointmentsByPatientAsync(patientId);
        if (appointment == null || appointment.ToArray().Length == 0 || appointment.ToArray().Last().Finished)
        {
            throw new Exception("No appointment found for the user");
        }
        patient.NurseId = nurse.Id;
        await _patientRepository.UpdateAsync(patient);
        return patient;
    }

    public async Task<Patient> AssociateDoctorToPatientAsync(int patientId, int nurseId)
    {
        var patient = await GetPatientByIdAsync(patientId);
        if (patient == null || patient.NurseId == null || patient.DoctorId != null)
        {
            throw new Exception("Patient not found");
        }

        var nurse = await _workerService.GetWorkerByIdAsync(nurseId);
        if (nurse == null || nurse.Type != WorkerType.Doctor)
        {
            throw new Exception("Worker not found or is not a nurse");
        }

        var appointment = await _appointmentService.GetAppointmentsByPatientAsync(patientId);
        if (appointment == null || appointment.ToArray().Length == 0 || appointment.ToArray().Last().Finished)
        {
            throw new Exception("No appointment found for the user");
        }
        patient.NurseId = null;
        patient.DoctorId = nurse.Id;
        await _patientRepository.UpdateAsync(patient);
        return patient;
    }
}