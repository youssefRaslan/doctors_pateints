using doctors.DTO;
using doctors.services.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace doctors.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private IDoctorService _doctorService;
        private readonly data.AppDbContext _context;

        public DoctorController(IDoctorService doctorService, data.AppDbContext context)
        {
            _doctorService = doctorService;
            _context = context;
        }

        [HttpGet("v1/getalldoctors")]
        [ProducesResponseType(typeof(GetallDoctorDTO), 200)] 
        [ProducesResponseType(typeof(Help.Error), 404)]
        [ProducesResponseType(typeof(Help.Error), 500)]
        public IActionResult GetAll()
        {
            var doctors = _doctorService.GetAll();
            return Ok(doctors);
        }
        [HttpPost("v1/adddoctor")]
        [ProducesResponseType(typeof(requstDoctorDTO), 200)]
        [ProducesResponseType(typeof(Help.Error), 400)]
        [ProducesResponseType(typeof(Help.Error), 500)]
        public async Task<ActionResult<requstDoctorDTO>> Adddoctor([FromForm] AddDoctorDTO doctor)
        {
            if (doctor == null)
            {
                return BadRequest(new Help.Error
                {
                    Message = "Doctor data is required",
                    StatusCode = 400
                });
            }

            var result = await _doctorService.Add(doctor);

            if (result == null)
            {
                return BadRequest(new Help.Error
                {
                    Message = "Email already exists",
                    StatusCode = 400
                });
            }

            return Ok(result);
        }

        [HttpPost("verify-email")]
        public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailDTO model)
        {
            var doctor = await _context.doctors
                .FirstOrDefaultAsync(d => d.Email == model.Email);

            if (doctor == null)
                return NotFound("Doctor not found.");

            if (doctor.VerificationCode != model.VerificationCode)
                return BadRequest("Invalid verification code.");

    
            doctor.IsEmailVerified = true;
            doctor.VerificationCode = null;
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Email verified successfully!",
                doctorId = doctor.Id,
                name = doctor.Name
            });
        }

        [HttpPost("v1/getgmaildoctor")]
        [ProducesResponseType(typeof(reqstgmailDoctorDTO), 200)] 
        [ProducesResponseType(typeof(Help.Error), 404)]
        [ProducesResponseType(typeof(Help.Error), 500)]
        public async Task <ActionResult<reqstgmailDoctorDTO>> Getgmail([FromBody] getgmailDoctorDTO getgmail)
        {
            if (getgmail == null)
            {
                return BadRequest(new Help.Error
                {
                    Message = "Gmail data is required",
                    StatusCode = 400
                });
            }
       var resuit = await _doctorService.Getgmail(getgmail);
               return Ok(resuit);
        }
        [HttpDelete("v1/removedoctor")]
        public async Task<ActionResult<removeDoctorDTO>> removedoc([FromBody] getgmailDoctorDTO removeDOC) { 
         if (removeDOC == null)
                return BadRequest(new Help.Error { Message = "Gmail data is required", StatusCode = 400 });

            var resuit = await _doctorService.Remove(removeDOC);
            return Ok(resuit);

        }
        [HttpPatch("v1/updatedoctor{id}")]
        public async Task<ActionResult<updatDoctorDTO>> updatDOC(int id, [FromBody] updatDoctorDTO updDOC) {
            if (updDOC == null)
                return BadRequest(new Help.Error { Message = "Update data is required", StatusCode = 400 });
            var updat = new rqstupdatDoctorDTO { id = id };
            var resuit = await _doctorService.Update(updat, updDOC);
            return Ok(resuit);
        }



    }
}
