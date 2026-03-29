namespace doctors.DTO
{
    public class PatientDetailsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? ImageUrl { get; set; }
        public System.DateOnly? BirthDate { get; set; }
        public int? Age { get; set; }
    }

    public class UpdatePatientDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string? Address { get; set; }
        public System.DateOnly? BirthDate { get; set; }
    }
}
