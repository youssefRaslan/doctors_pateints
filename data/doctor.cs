using System.ComponentModel.DataAnnotations;

namespace doctors.data
{
    public class doctor
    {

        public int Id { get; set; }
        [MaxLength(20)]
        [MinLength(3)]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Name must contain letters only.")]
        public string Name { get; set; }
        [MaxLength(20)]
        [MinLength(3)]
        public string Specialty { get; set; }
        [Required(ErrorMessage = "u have add a phone")]
        [MinLength(11)]
        public int PhoneNumber { get; set; }
        [Required(ErrorMessage = " u have to add email")]
        [EmailAddress]
        public string Email { get; set; }
        public string? Image { get; set; }
        [MaxLength(20)]
        public string? Address { get; set; }
        public string? VerificationCode { get; set; }

        public bool IsEmailVerified { get; set; } = false;
        public ICollection<PatientDoctor> PatientDoctors { get; set; }

    }
}
