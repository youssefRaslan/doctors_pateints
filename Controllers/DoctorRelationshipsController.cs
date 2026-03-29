using doctors.DTO;
using doctors.services.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace doctors.Controllers
{
    [Route("api/doctor")]
    [ApiController]
    [Authorize(Roles = "Doctor")]
    public class DoctorRelationshipsController : ControllerBase
    {
        private readonly IDoctorPatientService _service;

        public DoctorRelationshipsController(IDoctorPatientService service)
        {
            _service = service;
        }

        [HttpPost("send-request")]
        public async Task<IActionResult> SendRequest([FromBody] SendRequestDTO request)
        {
            if (string.IsNullOrWhiteSpace(request.PatientEmail) || string.IsNullOrWhiteSpace(request.PatientPhone))
                return BadRequest("Patient Email and Phone are required.");

            var success = await _service.SendRequestAsync(request.DoctorId, request.PatientEmail, request.PatientPhone);
            if (!success)
                return BadRequest("Request could not be sent. Invalid patient data, duplicate request, or relationship already exists.");

            return Ok(new { message = "Request sent successfully." });
        }

        [HttpGet("patients")]
        public async Task<IActionResult> GetAcceptedPatients([FromQuery] int doctorId)
        {
            var patients = await _service.GetAcceptedPatientsAsync(doctorId);
            return Ok(patients);
        }

        [HttpGet("sent-requests")]
        public async Task<IActionResult> GetSentRequests([FromQuery] int doctorId)
        {
            var requests = await _service.GetSentRequestsAsync(doctorId);
            return Ok(requests);
        }

        [HttpDelete("remove-patient/{patientId}")]
        public async Task<IActionResult> RemovePatient(int patientId, [FromQuery] int doctorId)
        {
            var success = await _service.RemovePatientAsync(doctorId, patientId);
            if (!success) return NotFound("Active relationship not found.");

            return Ok(new { message = "Patient removed successfully." });
        }
    }
}