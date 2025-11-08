using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace doctors.data
{
    public class Patient
    {

        public int Id { get; set; }
        [MaxLength(20)]
        [MinLength(3)]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Name must contain letters only.")]
        public string Name { get; set; }
        [MaxLength(20)]
        public string Address { get; set; }
        [Required(ErrorMessage = "You have to add a phone number")]
        [MinLength(11)]
        public string PhoneNumber { get; set; }
        public string Image { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "You have to add an email")]
        public string email { get; set; }


        public ICollection<PatientDoctor> PatientDoctor { get; set; }
    }
}
