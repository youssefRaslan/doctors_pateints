using System.Threading.Tasks;
using doctors.DTO;
using doctors.services.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace doctors.Controllers
{
    [Route("api/patient")]
    [ApiController]
    [Authorize]
    public class PatientController : ControllerBase
    {
        private readonly IpatientService _patientService;

        public PatientController(IpatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var patient = await _patientService.GetByIdAsync(id);
            if (patient == null) return NotFound(new { message = "Patient not found" });

            return Ok(patient);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var patients = await _patientService.GetAllAsync(page, pageSize);
            return Ok(patients);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdatePatientDTO model)
        {
            var success = await _patientService.UpdateAsync(model.Id, model);
            if (!success) return NotFound(new { message = "Patient not found" });

            return Ok(new { message = "Patient updated successfully" });
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _patientService.RemoveAsync(id);
            if (!success) return NotFound(new { message = "Patient not found" });

            return Ok(new { message = "Patient deleted successfully" });
        }
    }
}