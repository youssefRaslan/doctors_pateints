using System;

namespace doctors.data
{
    public class Measurement
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public Patient? Patient { get; set; }
        
        public double SugarLevel { get; set; }
        public double BloodPressure { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
}