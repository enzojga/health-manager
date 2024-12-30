
using backend.DTOS.WorkerDto;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkerController : ControllerBase
    {
        private readonly WorkerService _workerService;

        public WorkerController(WorkerService workerService)
        {
            _workerService = workerService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Worker>>> GetAllWorkers([FromQuery] string type, [FromQuery] bool? available)
        {
            if (!Enum.TryParse<WorkerType>(type, true, out var workerTypeEnum) || !Enum.IsDefined(typeof(WorkerType), workerTypeEnum))
            {
                return BadRequest();
            }
            var workers = await _workerService.GetAllWorkersAsync(workerTypeEnum, available);
            return Ok(workers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Worker>> GetWorkerById(int id)
        {
            var worker = await _workerService.GetWorkerByIdAsync(id);
            if (worker == null)
            {
                return NotFound();
            }
            return Ok(worker);
        }

        [HttpPost]
        public async Task<ActionResult<WorkerDto>> CreateWorker(WorkerDto workerDto)
        {
            var worker = await _workerService.CreateWorkerAsync(workerDto);
            return CreatedAtAction(nameof(GetWorkerById), new { id = worker.Id }, worker);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWorker(int id, WorkerDto workerDto)
        {
            var result = await _workerService.UpdateWorkerAsync(id, workerDto);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorker(int id)
        {
            var result = await _workerService.DeleteWorkerAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}