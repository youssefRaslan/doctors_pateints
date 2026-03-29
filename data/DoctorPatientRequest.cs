using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace doctors.data
{
    public class DoctorPatientRequest
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        public doctor? Doctor { get; set; }

        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public Patient? Patient { get; set; }

        public string Status { get; set; } = "Pending"; // Pending, Accepted, Rejected
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}