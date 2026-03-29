using doctors.data;
using doctors.DTO;
using doctors.services.interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using Microsoft.IdentityModel.Tokens;

namespace doctors.services.implementation
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly Icloudinarycs _cloudinary;
        private readonly IConfiguration _configuration;

        public AuthService(AppDbContext context, Icloudinarycs cloudinary, IConfiguration configuration)
        {
            _context = context;
            _cloudinary = cloudinary;
            _configuration = configuration;
        }

        public async Task<AuthResponseDto> RegisterDoctorAsync(RegisterUserDto model)
        {
            if (await UserExistsAsync(model.Email, model.Phone))
                return new AuthResponseDto { Success = false, Message = "Email or Phone already exists." };

            string? imageUrl = null;
            if (model.Photo != null)
            {
                var uploadResult = await _cloudinary.UploadImageAsync(model.Photo);
                if (uploadResult != null)
                {
                    imageUrl = uploadResult.Url;
                }
            }

            var doctor = new doctor        
            {
                Name = model.Name,
                Email = model.Email,
                PhoneNumber = model.Phone,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password),
                Role = "Doctor",
                IsEmailVerified = false,
                VerificationCode = GenerateVerificationCode(),
                Specialty = "General", // Or another default, as your model requires it
                Image = imageUrl,
            };

            _context.doctors.Add(doctor);
            await _context.SaveChangesAsync();

            // TODO: Trigger EmailService here to send doctor.VerificationCode

            return new AuthResponseDto { Success = true, Message = "Doctor registered successfully. Please verify your email." };
        }

        public async Task<AuthResponseDto> RegisterPatientAsync(RegisterUserDto model)
        {
            if (await UserExistsAsync(model.Email, model.Phone))
                return new AuthResponseDto { Success = false, Message = "Email or Phone already exists." };

            string? imageUrl = null;
            if (model.Photo != null)
            {
                var uploadResult = await _cloudinary.UploadImageAsync(model.Photo);
                if (uploadResult != null)
                {
                    imageUrl = uploadResult.Url;
                }
            }

            var patient = new Patient
            {
                Name = model.Name,
                Email = model.Email,
                PhoneNumber = model.Phone,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password),
                Role = "Patient",
                ImageUrl = imageUrl
                // IsEmailVerified and VerificationCode removed because Patient doesn't have these
            };

            _context.patients.Add(patient);
            await _context.SaveChangesAsync();

            // TODO: Trigger EmailService here to send patient.VerificationCode

            return new AuthResponseDto { Success = true, Message = "Patient registered successfully." };
        }

        public async Task<AuthResponseDto> VerifyEmailAsync(VerifyEmailDto model, bool isDoctor)
        {
            if (isDoctor)
            {
                var doctor = await _context.doctors.FirstOrDefaultAsync(d => d.Email == model.Email);
                if (doctor == null || doctor.VerificationCode != model.VerificationCode)
                    return new AuthResponseDto { Success = false, Message = "Invalid verification code or user." };

                doctor.IsEmailVerified = true;
                doctor.VerificationCode = null;
            }
            else
            {
                var patient = await _context.patients.FirstOrDefaultAsync(p => p.Email == model.Email);
                if (patient == null)
                    return new AuthResponseDto { Success = false, Message = "Invalid user." };
            }

            await _context.SaveChangesAsync();
            return new AuthResponseDto { Success = true, Message = "Email verified successfully!" };
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto model)
        {
            // First check Doctors
            var doctor = await _context.doctors.FirstOrDefaultAsync(d => d.Email == model.Email);
            if (doctor != null)
            {
                if (!BCrypt.Net.BCrypt.Verify(model.Password, doctor.PasswordHash))
                    return new AuthResponseDto { Success = false, Message = "Invalid credentials." };

                if (!doctor.IsEmailVerified)
                    return new AuthResponseDto { Success = false, Message = "Please verify your email first." };

                return GenerateAuthSuccessResponse(doctor.Id, doctor.Name, doctor.Email, "Doctor");
            }

            // Then check Patients
            var patient = await _context.patients.FirstOrDefaultAsync(p => p.Email == model.Email);
            if (patient != null)
            {
                if (!BCrypt.Net.BCrypt.Verify(model.Password, patient.PasswordHash))
                    return new AuthResponseDto { Success = false, Message = "Invalid credentials." };

                return GenerateAuthSuccessResponse(patient.Id, patient.Name, patient.Email, "Patient");
            }

            return new AuthResponseDto { Success = false, Message = "User not found." };
        }

        // --- Helper Methods ---
        
        private async Task<bool> UserExistsAsync(string email, string phone)
        {
            return await _context.doctors.AnyAsync(d => d.Email == email || d.PhoneNumber == phone) || 
                   await _context.patients.AnyAsync(p => p.Email == email || p.PhoneNumber == phone);
        }

        private string GenerateVerificationCode()
        {
            return RandomNumberGenerator.GetInt32(100000, 999999).ToString(); // Generates 6-digit code
        }

        private AuthResponseDto GenerateAuthSuccessResponse(int id, string name, string email, string role)
        {
            var keyStr = _configuration["Jwt:Key"] ?? "a_very_long_super_secret_key_which_is_secure_1234567890";
            var issuer = _configuration["Jwt:Issuer"] ?? "DoctorsApp";
            var audience = _configuration["Jwt:Audience"] ?? "DoctorsApp";

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, id.ToString()),
                new(ClaimTypes.Name, name),
                new(ClaimTypes.Email, email),
                new(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyStr));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddDays(7);

            var jwtToken = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return new AuthResponseDto
            {
                Success = true,
                Message = "Login successful",
                Token = token,
                User = new { id, name, email, role }
            };
        }
    }
}