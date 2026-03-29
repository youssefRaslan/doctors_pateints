using System.Threading.Tasks;
using doctors.DTO;
using doctors.services.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace doctors.Controllers
{
    [Route("api/measurement")]
    [ApiController]
    [Authorize]
    public class MeasurementController : ControllerBase
    {
        private readonly IMeasurementService _service;

        public MeasurementController(IMeasurementService service)
        {
            _service = service;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddMeasurement([FromBody] AddMeasurementDTO model)
        {
            var success = await _service.AddMeasurementAsync(model);
            if (!success) return BadRequest(new { message = "Patient not found." });

            return Ok(new { message = "Measurement added successfully." });
        }

        [HttpGet("{patientId}")]
        public async Task<IActionResult> GetMeasurements(int patientId)
        {
            var measurements = await _service.GetPatientMeasurementsAsync(patientId);
            return Ok(measurements);
        }
    }
}