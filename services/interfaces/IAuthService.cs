using System.Threading.Tasks;
using doctors.DTO;

namespace doctors.services.interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterDoctorAsync(RegisterUserDto model);
        Task<AuthResponseDto> RegisterPatientAsync(RegisterUserDto model);
        Task<AuthResponseDto> VerifyEmailAsync(VerifyEmailDto model, bool isDoctor);
        Task<AuthResponseDto> LoginAsync(LoginDto model);
    }
}