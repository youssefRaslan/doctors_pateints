using System.Collections.Generic;
using System.Threading.Tasks;
using doctors.DTO;

namespace doctors.services.interfaces
{
    public interface IDoctorPatientService
    {
        // Doctor APIs
        Task<bool> SendRequestAsync(int doctorId, string patientEmail, string patientPhone);
        Task<IEnumerable<PatientViewDTO>> GetAcceptedPatientsAsync(int doctorId);
        Task<IEnumerable<RequestViewDTO>> GetSentRequestsAsync(int doctorId);
        Task<bool> RemovePatientAsync(int doctorId, int patientId);

        // Patient APIs
        Task<IEnumerable<RequestViewDTO>> GetIncomingRequestsAsync(int patientId);
        Task<bool> AcceptRequestAsync(int patientId, int requestId);
        Task<bool> RejectRequestAsync(int patientId, int requestId);
        Task<bool> RemoveDoctorAsync(int patientId, int doctorId);
    }
}