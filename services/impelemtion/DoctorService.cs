using doctors.data;
using doctors.DTO;
using doctors.services.interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;
namespace doctors.services.impelemtion
{
    public class DoctorService : IDoctorService
    {
        private readonly AppDbContext _context;
        private readonly Icloudinarycs _cloudinaryService;
        private readonly IEmail _emailService;
        public DoctorService(AppDbContext context, Icloudinarycs cloudinaryService, IEmail emailService)
        {

            _context = context;
            _cloudinaryService = cloudinaryService;
            _emailService = emailService;
        }
        public async Task<reqstgmailDoctorDTO> Getgmail(getgmailDoctorDTO getgmail)
        {
            var doctor = await _context.doctors
                .FirstOrDefaultAsync(d => d.Email == getgmail.gmail);
            if (doctor == null)
           
                return null;


            return new reqstgmailDoctorDTO
            {
                id = doctor.Id,
                name = doctor.Name,
                ImageFile = doctor.Image
            };
        }


        public List<GetallDoctorDTO> GetAll()
        {
            var doctors = _context.doctors
             .Select(d => new GetallDoctorDTO
             {
                 specialization = d.Specialty,
                 name = d.Name,
                 Email = d.Email,
                 PhoneNumber = d.PhoneNumber,

             })
             .ToList();

            return doctors;
        }
        public async Task<requstDoctorDTO> Add(AddDoctorDTO doctor1)
        {
           
            var gmailExists = await _context.doctors.AnyAsync(d => d.Email == doctor1.Email);
            if (gmailExists)
            {
                return null;
            }

            
            string verificationCode = new Random().Next(100000, 999999).ToString();

            
            string imageUrl = null;
            if (doctor1.ImageFile != null && doctor1.ImageFile.Length > 0)
            {
                var uploadResult = await _cloudinaryService.UploadImageAsync(doctor1.ImageFile);
                imageUrl = uploadResult.Url;
            }

           
            var newDoctor = new doctor
            {
                Name = doctor1.name,
                Specialty = doctor1.specialization,
                Email = doctor1.Email,
                PhoneNumber = doctor1.PhoneNumber,
                Address = doctor1.Address,
                Image = imageUrl,
                VerificationCode = verificationCode, 
                IsEmailVerified = false 
            };

            _context.doctors.Add(newDoctor);
            await _context.SaveChangesAsync();

        
            await _emailService.SendEmailAsync(doctor1.Email, verificationCode);

            return new requstDoctorDTO
            {
                id = newDoctor.Id,
                name = newDoctor.Name,
                email = newDoctor.Email,
                message = "Verification code sent to your email"
            };
        }
        public async Task<bool> VerifyEmail(string email, string code)
        {
            var doctor = await _context.doctors.FirstOrDefaultAsync(d => d.Email == email);
            if (doctor == null) return false;
            if (doctor.VerificationCode != code) return false;

            doctor.IsEmailVerified = true;
            doctor.VerificationCode = null;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task <removeDoctorDTO> Remove(getgmailDoctorDTO removeDOC)
        {
            var doctor = await _context.doctors.FirstOrDefaultAsync(d => d.Email == removeDOC.gmail);
            if (doctor == null)
            
                return null;

            _context.doctors.Remove(doctor);
            await _context.SaveChangesAsync();
            return  new removeDoctorDTO
            {
                massage = "doctor removed done"
            };
           

        }

        public async Task <updatDoctorDTO> Update(rqstupdatDoctorDTO id,updatDoctorDTO updat)
        {
            var doctor = await _context.doctors.FirstOrDefaultAsync(d => d.Id == id.id);
            if (doctor == null)
            
                return null;
            _context.doctors.Update(doctor);
            await _context.SaveChangesAsync();
            return new updatDoctorDTO { 
            image= updat.image,
            phoneNumber= updat.phoneNumber,
            specialization= updat.specialization
};
            

        }
    }
}
