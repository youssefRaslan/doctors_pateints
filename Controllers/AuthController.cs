using System.Threading.Tasks;
using doctors.DTO;
using doctors.services.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace doctors.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register/doctor")]
        public async Task<IActionResult> RegisterDoctor([FromForm] RegisterUserDto model)
        {
            var result = await _authService.RegisterDoctorAsync(model);
            
            if (!result.Success) return BadRequest(new { message = result.Message });
            return Ok(result);
        }

        [HttpPost("register/patient")]
        public async Task<IActionResult> RegisterPatient([FromForm] RegisterUserDto model)
        {
            var result = await _authService.RegisterPatientAsync(model);
            if (!result.Success) return BadRequest(new { message = result.Message });
            return Ok(result);
        }

        [HttpPost("verify-email")]
        public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailDto model, [FromQuery] string role)
        {
            if(string.IsNullOrEmpty(role) || (role.ToLower() != "doctor" && role.ToLower() != "patient"))
                return BadRequest(new { message = "Role must be defined as 'doctor' or 'patient'" });

            bool isDoctor = role.ToLower() == "doctor";
            var result = await _authService.VerifyEmailAsync(model, isDoctor);
            
            if (!result.Success) return BadRequest(new { message = result.Message });
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var result = await _authService.LoginAsync(model);
            if (!result.Success) return Unauthorized(new { message = result.Message });
            
            return Ok(result);
        }
    }
}