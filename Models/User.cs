using System.ComponentModel.DataAnnotations;

namespace HomeLoanAPI.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string? MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public DateTime Dob { get; set; }

        public string Gender { get; set; }
        public string Nationality { get; set; }
        public string AadharNo { get; set; }
        public string PanNo { get; set; }

        public string Role { get; set; } = "User";  // Default role
    }
}
