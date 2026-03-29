using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace doctors.data
{
    public class Patient
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        [RegularExpression(@"^[\p{L} \-']+$", ErrorMessage = "Name must contain letters, spaces, hyphens or apostrophes only.")]
        public string Name { get; set; } = string.Empty;

        [StringLength(200)]
        public string? Address { get; set; }

        [Required(ErrorMessage = "You have to add a phone number")]
        [Phone]
        [StringLength(20, MinimumLength = 7)]
        public string PhoneNumber { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "You have to add an email")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = "Patient";

        // Optional birth date to compute age in controller or U
        public DateOnly? BirthDate { get; set; }

        // Not mapped to the database; computed from BirthDate
        [NotMapped]
        public int? Age
        {
            get
            {
                if (!BirthDate.HasValue) return null;
                var today = DateOnly.FromDateTime(DateTime.UtcNow);
                var age = today.Year - BirthDate.Value.Year;
                if (today < BirthDate.Value.AddYears(age)) age--;
                return age;
            }
        }

        // Navigation - initialized to avoid null checks
        public ICollection<PatientDoctor> PatientDoctors { get; set; } = new List<PatientDoctor>();
    }
}
