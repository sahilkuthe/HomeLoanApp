using HomeLoanAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeLoanAPI.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }
        // Define your DbSets (tables) here
        public DbSet<User> Users { get; set; }
        public DbSet<LoanApplication> LoanApplications { get; set; }



    }
}
