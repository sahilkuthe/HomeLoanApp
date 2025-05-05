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

        public async Task<LoanApplication> TrackApplication(string appNumber, DateTime dob)
        {
            return await _context.LoanApplications
                .Include(l => l.User)
                .FirstOrDefaultAsync(l => l.ApplicationNumber == appNumber && l.Dob.Date == dob.Date);
        }


        public async Task<List<LoanApplication>> GetApplicationsByUserIdAsync(int userId)
        {
            return await _context.LoanApplications
                .Where(app => app.UserId == userId)
                .ToListAsync();
        }


        public async Task<bool> DeleteApplicationAsync(int applicationId, int userId)
        {
            var application = await _context.LoanApplications
                .FirstOrDefaultAsync(a => a.ApplicationId == applicationId && a.UserId == userId );

            if (application == null)
                return false;

            _context.LoanApplications.Remove(application);
            await _context.SaveChangesAsync();
            return true;
        }



        public async Task<LoanApplication> GetApplicationByIdAsync(int applicationId)
        {
            return await _context.LoanApplications
                                 .Include(a => a.User)
                                 .FirstOrDefaultAsync(a => a.ApplicationId == applicationId);
        }





    }
}
