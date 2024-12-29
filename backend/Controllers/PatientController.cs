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

    [HttpPut("{patientId}/associate-nurse/{nurseId}")]
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

    [HttpPut("{patientId}/associate-doctor/{doctorId}")]
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

    [HttpPut("{patientId}/associate-room/{roomId}")]
    public async Task<ActionResult> AssociateRoomToPatient(int patientId, int roomId)
    {
        try
        {
            await _patientService.AssociateRoomToPatientAsync(patientId, roomId);
            return Ok("Room associated to user successfully");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{patientId}/finish-appointment")]
    public async Task<ActionResult> FinishPatientAppointment(int patientId)
    {
        try
        {
            await _patientService.FinishPatientAppointmentAsync(patientId);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}