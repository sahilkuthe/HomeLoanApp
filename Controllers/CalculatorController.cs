using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomeLoanAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        // GET: api/calculator/eligibility?monthlyIncome=50000
        [HttpGet("eligibility")]
        public IActionResult CalculateEligibility([FromQuery] decimal monthlyIncome)
        {
            if (monthlyIncome <= 0)
                return BadRequest("Monthly income must be greater than zero.");

            var eligibleAmount = 60 * (0.6m * monthlyIncome);

            return Ok(new
            {
                MonthlyIncome = monthlyIncome,
                EligibleLoanAmount = Math.Round(eligibleAmount, 2)
            });
        }

        // GET: api/calculator/emi?loanAmount=1000000&tenureMonths=120
        [HttpGet("emi")]
        public IActionResult CalculateEMI([FromQuery] decimal loanAmount, [FromQuery] int tenureMonths)
        {
            if (loanAmount <= 0 || tenureMonths <= 0)
                return BadRequest("Loan amount and tenure must be greater than zero.");

            const decimal annualInterestRate = 8.5m;
            decimal monthlyRate = annualInterestRate / 12 / 100;

            var emi = loanAmount * monthlyRate * (decimal)Math.Pow((double)(1 + monthlyRate), tenureMonths) /
                      (decimal)(Math.Pow((double)(1 + monthlyRate), tenureMonths) - 1);

            return Ok(new
            {
                LoanAmount = loanAmount,
                TenureMonths = tenureMonths,
                MonthlyEMI = Math.Round(emi, 2)
            });
        }
    }
}
