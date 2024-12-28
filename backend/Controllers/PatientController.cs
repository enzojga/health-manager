using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class PatientController : ControllerBase
{
    private readonly PatientService _patientService;

    public PatientController(PatientService patientService)
    {
        _patientService = patientService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Patient>>> GetAllPatients()
    {
        var patients = await _patientService.GetAllPatientsAsync();
        return Ok(patients);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Patient>> GetPatientById(int id)
    {
        var patient = await _patientService.GetPatientByIdAsync(id);
        if (patient == null)
        {
            return NotFound();
        }
        return Ok(patient);
    }

    [HttpPost]
    public async Task<ActionResult<PatientDto>> CreatePatient(PatientDto patientDto)
    {
        var patient = await _patientService.CreatePatientAsync(patientDto);
        return CreatedAtAction(nameof(GetPatientById), new { id = patient.Id }, patient);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePatient(int id, PatientDto patientDto)
    {
        var result = await _patientService.UpdatePatientAsync(id, patientDto);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePatient(int id)
    {
        var result = await _patientService.DeletePatientAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpPost("{patientId}/associate-nurse/{nurseId}")]
    public async Task<ActionResult> AssociateNurseToPatient(int patientId, int nurseId)
    {
        try
        {
            await _patientService.AssociateNurseToPatientAsync(patientId, nurseId);
            return Ok("Nurse associated to user successfully");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{patientId}/associate-doctor/{doctorId}")]
    public async Task<ActionResult> AssociateDoctorToPatient(int patientId, int doctorId)
    {
        try
        {
            await _patientService.AssociateDoctorToPatientAsync(patientId, doctorId);
            return Ok("Doctor associated to user successfully");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}