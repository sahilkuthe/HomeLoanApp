namespace HomeLoanAPI.DTOs
{
    public class LoanApplicationDTO
    {
        public string PropertyLocation { get; set; }
        public string PropertyName { get; set; }
        public decimal EstimatedCost { get; set; }
        public string EmploymentType { get; set; }
        public int RetirementAge { get; set; }
        public string OrganizationType { get; set; }
        public string EmployerName { get; set; }
        public decimal NetMonthlyIncome { get; set; }

        public decimal MaxLoanGrantable { get; set; }
        public decimal InterestRate { get; set; }
        public int Tenure { get; set; }
        public decimal LoanAmount { get; set; }

        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Dob { get; set; }
        public string Gender { get; set; }
        public string Nationality { get; set; }
        public string AadharNo { get; set; }
        public string PanNo { get; set; }

        // IDs of the uploaded documents will come in a separate upload API
        public int UserId { get; set; }
    }
}
