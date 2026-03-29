using System;

namespace doctors.DTO
{
    public class AddMeasurementDTO
    {
        public int PatientId { get; set; }
        public double SugarLevel { get; set; }
        public double BloodPressure { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
    }

    public class MeasurementDTO
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public double SugarLevel { get; set; }
        public double BloodPressure { get; set; }
        public DateTime Date { get; set; }
    }
}