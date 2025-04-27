namespace HomeLoanAPI.DTOs
{
    public class RegisterDTO
    {
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Dob { get; set; }
        public string Gender { get; set; }
        public string Nationality { get; set; }
        public string AadharNo { get; set; }
        public string PanNo { get; set; }
    }
}
