using doctors.DTO;
using doctors.services.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;



namespace doctors.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
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
        [ProducesResponseType(typeof(Help.Error), 404)]
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
                var resuit = await _doctorService.Add(doctor);
            return CreatedAtAction(nameof(Adddoctor), new {  resuit.id },resuit);
        
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
