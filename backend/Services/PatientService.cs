public class PatientService
{
    private readonly IPatientRepository _patientRepository;

    public PatientService(IPatientRepository patientRepository, IAppointmentRepository appointmentRepository)
    {
        _patientRepository = patientRepository;
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
        patient.RoomId = patientDto.RoomId;
        patient.DoctorId = patientDto.DoctorId;
        patient.NurseId = patientDto.NurseId;
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
}