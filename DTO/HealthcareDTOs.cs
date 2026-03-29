using System;

namespace doctors.DTO
{
    public class SendRequestDTO
    {
        public string PatientEmail { get; set; } = string.Empty;
        public string PatientPhone { get; set; } = string.Empty;
        public int DoctorId { get; set; } 
    }

    public class RequestViewDTO
    {
        public int RequestId { get; set; }
        public int DoctorId { get; set; }
        public string DoctorName { get; set; } = string.Empty;
        public int PatientId { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }

    public class PatientViewDTO
    {
        public int PatientId { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public DateTime LinkedAt { get; set; }
    }

    public class DoctorViewDTO
    {
        public int DoctorId { get; set; }
        public string DoctorName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public DateTime LinkedAt { get; set; }
    }
}