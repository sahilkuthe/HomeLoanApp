using HomeLoanAPI.Data;
using HomeLoanAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeLoanAPI.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly ApplicationDBContext _context;

        public LoanRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<LoanApplication> SubmitApplication(LoanApplication app)
        {
            _context.LoanApplications.Add(app);
            await _context.SaveChangesAsync();
            return app;
        }

        public async Task<LoanApplication> GetByApplicationNumber(string appNumber)
        {
            return await _context.LoanApplications
                .Include(l => l.User)
                .FirstOrDefaultAsync(l => l.ApplicationNumber == appNumber);
        }

    }
}
