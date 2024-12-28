using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly AppointmentService _appointmentService;

    public AppointmentsController(AppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    [HttpPost]
    public async Task<IActionResult> AddAppointmentAsync([FromBody] AppointmentDto appointmentDto)
    {
        var appointment = await _appointmentService.AddAppointmentAsync(appointmentDto);
        if (appointment == null)
        {
            return BadRequest("Unable to add appointment.");
        }
        return Ok(appointment);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAppointmentAsync(int id, [FromBody] AppointmentDto appointmentDto)
    {
        var result = await _appointmentService.UpdateAppointmentAsync(id, appointmentDto);
        if (!result)
        {
            return NotFound("Appointment not found.");
        }
        return Ok("Appointment updated successfully.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAppointmentAsync(int id)
    {
        var result = await _appointmentService.DeleteAppointmentAsync(id);
        if (!result)
        {
            return NotFound("Appointment not found.");
        }
        return NoContent();
    }

        [HttpGet("user/{userId}")]
    public async Task<ActionResult<Appointment>> GetAppointmentsByUserIdAsync(int userId)
    {
        var appointments = await _appointmentService.GetAppointmentsByPatientAsync(userId);
        if (appointments == null)
        {
            return NotFound("No appointments found for the user.");
        }
        return Ok(appointments);
    }
}