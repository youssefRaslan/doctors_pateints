using doctors.data;
using doctors.DTO;
using doctors.services.interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace doctors.services.impelemtion
{
    public class DoctorPatientService : IDoctorPatientService
    {
        private readonly AppDbContext _context;

        public DoctorPatientService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SendRequestAsync(int doctorId, string patientEmail, string patientPhone)
        {
            var patient = await _context.patients
                .FirstOrDefaultAsync(p => p.Email == patientEmail && p.PhoneNumber == patientPhone);

            if (patient == null) return false;

            // Check if there is an existing pending or accepted request
            var existingRequest = await _context.DoctorPatientRequests
                .FirstOrDefaultAsync(r => r.DoctorId == doctorId && r.PatientId == patient.Id 
                                     && (r.Status == "Pending" || r.Status == "Accepted"));

            if (existingRequest != null) return false;

            // Check if relationship already exists
            var existingRelation = await _context.PatientDoctors
                .FirstOrDefaultAsync(pd => pd.DoctorId == doctorId && pd.PatientId == patient.Id && pd.IsActive);
            
            if (existingRelation != null) return false;

            var request = new DoctorPatientRequest
            {
                DoctorId = doctorId,
                PatientId = patient.Id,
                Status = "Pending"
            };

            _context.DoctorPatientRequests.Add(request);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<PatientViewDTO>> GetAcceptedPatientsAsync(int doctorId)
        {
            return await _context.PatientDoctors
                .Where(pd => pd.DoctorId == doctorId && pd.IsActive)
                .Include(pd => pd.Patient)
                .Select(pd => new PatientViewDTO
                {
                    PatientId = pd.PatientId,
                    PatientName = pd.Patient!.Name,
                    Email = pd.Patient.Email,
                    Phone = pd.Patient.PhoneNumber,
                    LinkedAt = pd.CreatedAt
                }).ToListAsync();
        }

        public async Task<IEnumerable<RequestViewDTO>> GetSentRequestsAsync(int doctorId)
        {
            return await _context.DoctorPatientRequests
                .Where(r => r.DoctorId == doctorId)
                .Include(r => r.Patient)
                .Select(r => new RequestViewDTO
                {
                    RequestId = r.Id,
                    DoctorId = r.DoctorId,
                    PatientId = r.PatientId,
                    PatientName = r.Patient!.Name,
                    Status = r.Status,
                    CreatedAt = r.CreatedAt
                }).ToListAsync();
        }

        public async Task<bool> RemovePatientAsync(int doctorId, int patientId)
        {
            var relation = await _context.PatientDoctors
                .FirstOrDefaultAsync(pd => pd.DoctorId == doctorId && pd.PatientId == patientId && pd.IsActive);

            if (relation == null) return false;

            relation.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<RequestViewDTO>> GetIncomingRequestsAsync(int patientId)
        {
            return await _context.DoctorPatientRequests
                .Where(r => r.PatientId == patientId && r.Status == "Pending")
                .Include(r => r.Doctor)
                .Select(r => new RequestViewDTO
                {
                    RequestId = r.Id,
                    DoctorId = r.DoctorId,
                    DoctorName = r.Doctor!.Name,
                    PatientId = r.PatientId,
                    Status = r.Status,
                    CreatedAt = r.CreatedAt
                }).ToListAsync();
        }

        public async Task<bool> AcceptRequestAsync(int patientId, int requestId)
        {
            var request = await _context.DoctorPatientRequests
                .FirstOrDefaultAsync(r => r.Id == requestId && r.PatientId == patientId && r.Status == "Pending");

            if (request == null) return false;

            request.Status = "Accepted";

            // Add to active relationships
            var relation = await _context.PatientDoctors
                .FirstOrDefaultAsync(pd => pd.DoctorId == request.DoctorId && pd.PatientId == patientId);

            if (relation != null)
            {
                relation.IsActive = true;
                relation.CreatedAt = System.DateTime.UtcNow;
            }
            else
            {
                _context.PatientDoctors.Add(new PatientDoctor
                {
                    DoctorId = request.DoctorId,
                    PatientId = patientId,
                    IsActive = true,
                    CreatedAt = System.DateTime.UtcNow
                });
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RejectRequestAsync(int patientId, int requestId)
        {
            var request = await _context.DoctorPatientRequests
                .FirstOrDefaultAsync(r => r.Id == requestId && r.PatientId == patientId && r.Status == "Pending");

            if (request == null) return false;

            request.Status = "Rejected";
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveDoctorAsync(int patientId, int doctorId)
        {
            var relation = await _context.PatientDoctors
                .FirstOrDefaultAsync(pd => pd.DoctorId == doctorId && pd.PatientId == patientId && pd.IsActive);

            if (relation == null) return false;

            relation.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}