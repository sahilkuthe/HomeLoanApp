namespace HomeLoanAPI.DTOs
{
    public class LoanStatusUpdateDTO
    {
        public int ApplicationId { get; set; }
        public string Status { get; set; } // e.g., Approved, Rejected, UnderReview
        public string? Remarks { get; set; } // Optional comment by reviewer
    }
}
