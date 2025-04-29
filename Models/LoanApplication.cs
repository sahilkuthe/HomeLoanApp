using System.ComponentModel.DataAnnotations;

namespace HomeLoanAPI.Models
{
    public class LoanApplication
    {
        [Key]
        public int ApplicationId { get; set; }

        public string ApplicationNumber { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; } = "Sent for verification";

        // Income Details
        public string PropertyLocation { get; set; }
        public string PropertyName { get; set; }
        public decimal EstimatedCost { get; set; }
        public string EmploymentType { get; set; }  // Salaried/Self-employed
        public int RetirementAge { get; set; }
        public string OrganizationType { get; set; }
        public string EmployerName { get; set; }
        public decimal NetMonthlyIncome { get; set; }

        // Loan Details
        public decimal MaxLoanGrantable { get; set; }
        public decimal InterestRate { get; set; }
        public int Tenure { get; set; }
        public decimal LoanAmount { get; set; }

        // Personal Details
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

        // Document File Paths (storing file names or URLs)
        public string PanCardPath { get; set; }
        public string VoterIdPath { get; set; }
        public string SalarySlipPath { get; set; }
        public string LOAPath { get; set; }
        public string NOCPath { get; set; }
        public string AgreementPath { get; set; }

        // Foreign Key to User
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
