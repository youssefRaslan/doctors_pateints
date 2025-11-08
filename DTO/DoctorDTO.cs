using doctors.data;
using System.ComponentModel.DataAnnotations;

namespace doctors.DTO
{
    public class DoctorDTO
    {
    }
    public class GetallDoctorDTO
    {
        public string name { get; set; }
        public string specialization { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }


    }
    public class AddDoctorDTO
    {
        [Required]
        public string name { get; set; }
        [Required]
        public string specialization { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
  
        [Range(0100000000, 0199999999)]
        public int PhoneNumber { get; set; }
        public string? Address { get; set; }
        public IFormFile ImageFile { get; set; }




    }
    public class requstDoctorDTO
    {
        public int id { get; set; }
        public string name { get; set; }

   

    }
    public class getgmailDoctorDTO
    {
        [EmailAddress]
        [Required]
        public string gmail { get; set; }


    }
    public class reqstgmailDoctorDTO
    {


        public int id { get; set; }
        public string name { get; set; }


    }
    public class removeDoctorDTO
    {

        public string massage { get; set; }

    }
    public class updatDoctorDTO
    {

        [Required(ErrorMessage = "Image URL is required")]
        [Url(ErrorMessage = "Invalid image URL")]
        public string image { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "Phone number must be 11 digits")]
        public string phoneNumber { get; set; }

        [Required(ErrorMessage = "Specialization is required")]
        [StringLength(100, ErrorMessage = "Specialization cannot exceed 100 characters")]
        public string specialization { get; set; }
    }
    public class rqstupdatDoctorDTO
    {
        public int id { get; set; }
    }



}



