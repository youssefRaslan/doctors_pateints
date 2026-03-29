using doctors.DTO;
using doctors.services.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace doctors.Controllers
{
    [Route("api/patient")]
    [ApiController]
    [Authorize(Roles = "Patient")]
    public class PatientRelationshipsController : ControllerBase
    {
        private readonly IDoctorPatientService _service;

        public PatientRelationshipsController(IDoctorPatientService service)
        {
            _service = service;
        }

        [HttpGet("requests")]
        public async Task<IActionResult> GetIncomingRequests([FromQuery] int patientId)
        {
            var requests = await _service.GetIncomingRequestsAsync(patientId);
            return Ok(requests);
        }

        [HttpPost("accept/{requestId}")]
        public async Task<IActionResult> AcceptRequest(int requestId, [FromQuery] int patientId)
        {
            var success = await _service.AcceptRequestAsync(patientId, requestId);
            if (!success) return BadRequest("Invalid request or already processed.");

            return Ok(new { message = "Request accepted successfully." });
        }

        [HttpPost("reject/{requestId}")]
        public async Task<IActionResult> RejectRequest(int requestId, [FromQuery] int patientId)
        {
            var success = await _service.RejectRequestAsync(patientId, requestId);
            if (!success) return BadRequest("Invalid request or already processed.");

            return Ok(new { message = "Request rejected successfully." });
        }

        [HttpDelete("remove-doctor/{doctorId}")]
        public async Task<IActionResult> RemoveDoctor(int doctorId, [FromQuery] int patientId)
        {
            var success = await _service.RemoveDoctorAsync(patientId, doctorId);
            if (!success) return NotFound("Active relationship not found.");

            return Ok(new { message = "Doctor removed successfully." });
        }
    }
}