using HomeLoanAPI.Models;

namespace HomeLoanAPI.Repositories
{
    public interface ILoanRepository
    {
        Task<LoanApplication> SubmitApplication(LoanApplication app);
        Task<LoanApplication> GetByApplicationNumber(string appNumber);
    }
}
