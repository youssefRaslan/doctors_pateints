namespace doctors.data
{
    public class PatientDoctor
    {
        public int PatientId { get; set; }
        public Patient? Patient { get; set; }
        public int DoctorId { get; set; }
        public doctor? Doctor { get; set; }
    }
}
