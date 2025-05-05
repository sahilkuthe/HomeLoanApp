using HomeLoanAPI.Models;

namespace HomeLoanAPI.Repositories
{
    public interface ILoanRepository
    {
        Task<LoanApplication> SubmitApplication(LoanApplication app);
        Task<LoanApplication> GetByApplicationNumber(string appNumber);
        Task<LoanApplication> TrackApplication(string appNumber, DateTime dob);
        Task<List<LoanApplication>> GetApplicationsByUserIdAsync(int userId);
        Task<LoanApplication> GetApplicationByIdAsync(int applicationId);
        Task<bool> DeleteApplicationAsync(int applicationId, int userId);

    }
}
