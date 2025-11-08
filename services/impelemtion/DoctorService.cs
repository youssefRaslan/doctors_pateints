using doctors.data;
using doctors.DTO;
using doctors.services.interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace doctors.services.impelemtion
{
    public class DoctorService : IDoctorService
    {
        private readonly AppDbContext _context;
        private readonly Icloudinarycs _cloudinaryService;
        public DoctorService(AppDbContext context, Icloudinarycs cloudinaryService)
        {
           
            _context = context;
            _cloudinaryService = cloudinaryService;
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
                name = doctor.Name
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
        public async Task<requstDoctorDTO> Add(AddDoctorDTO doctor)
        {
            if (doctor.ImageFile == null)
                return null;
            // FIX: Use the instance 'doctor.ImageFile' instead of 'AddDoctorDTO.ImageFile'
            var uploadResult = await _cloudinaryService.UploadImageAsync(doctor.ImageFile);
            string imageUrl = uploadResult?.Url; // Assuming UploadImageResult has a Url property
            var newDoctor = new doctor
            {
                Name = doctor.name,
                Specialty = doctor.specialization,
                Email = doctor.Email,
                PhoneNumber = doctor.PhoneNumber,
                Address = doctor.Address,
                Image = imageUrl
            };
            _context.doctors.Add(newDoctor);
            await _context.SaveChangesAsync();
            return new requstDoctorDTO
            {
                id = newDoctor.Id,
                name = newDoctor.Name,
            };
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
