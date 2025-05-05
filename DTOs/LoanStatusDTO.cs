namespace HomeLoanAPI.DTOs
{
    public class LoanStatusDTO
    {
        public string ApplicationNumber { get; set; }
        public string Status { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string ApplicantName { get; set; }
        public string Email { get; set; }
    }
}
