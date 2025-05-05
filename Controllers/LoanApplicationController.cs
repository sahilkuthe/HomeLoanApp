using HomeLoanAPI.Data;
using HomeLoanAPI.DTOs;
using HomeLoanAPI.Models;
using HomeLoanAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeLoanAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanApplicationController : ControllerBase
    {
        private readonly ILoanRepository _loanRepo;
        private readonly ApplicationDBContext _context;


        public LoanApplicationController(ILoanRepository loanRepo, ApplicationDBContext context)
        {
            _loanRepo = loanRepo;
            _context = context;
        }

        [HttpPost("submit")]
        public async Task<IActionResult> SubmitApplication(LoanApplicationDTO dto)
        {
            var application = new LoanApplication
            {
                PropertyLocation = dto.PropertyLocation,
                PropertyName = dto.PropertyName,
                EstimatedCost = dto.EstimatedCost,
                EmploymentType = dto.EmploymentType,
                RetirementAge = dto.RetirementAge,
                OrganizationType = dto.OrganizationType,
                EmployerName = dto.EmployerName,
                NetMonthlyIncome = dto.NetMonthlyIncome,
                MaxLoanGrantable = dto.MaxLoanGrantable,
                InterestRate = dto.InterestRate,
                Tenure = dto.Tenure,
                LoanAmount = dto.LoanAmount,
                FirstName = dto.FirstName,
                MiddleName = dto.MiddleName,
                LastName = dto.LastName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Dob = dto.Dob,
                Gender = dto.Gender,
                Nationality = dto.Nationality,
                AadharNo = dto.AadharNo,
                PanNo = dto.PanNo,
                UserId = dto.UserId,

                // Auto generate these
                ApplicationNumber = "APP" + Guid.NewGuid().ToString("N").Substring(0, 10).ToUpper(),
                AppointmentDate = DateTime.Now.AddDays(3)
            };

            var result = await _loanRepo.SubmitApplication(application);

            return Ok(new
            {
                result.ApplicationNumber,
                result.AppointmentDate,
                Message = "Application submitted successfully!"
            });
        }



        [HttpPost("upload")]
        public IActionResult UploadDocuments([FromForm] DocumentUploadDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Example: Save files to wwwroot/uploads/
            var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", model.ApplicationNumber);
            Directory.CreateDirectory(uploadsPath);

            var panCardPath = Path.Combine(uploadsPath, "PanCard_" + model.PanCard.FileName);
            var aadharCardPath = Path.Combine(uploadsPath, "AadharCard_" + model.AadharCard.FileName);
            var salarySlipPath = Path.Combine(uploadsPath, "SalarySlip_" + model.SalarySlip.FileName);

            using (var stream = new FileStream(panCardPath, FileMode.Create))
            {
                model.PanCard.CopyTo(stream);
            }
            using (var stream = new FileStream(aadharCardPath, FileMode.Create))
            {
                model.AadharCard.CopyTo(stream);
            }
            using (var stream = new FileStream(salarySlipPath, FileMode.Create))
            {
                model.SalarySlip.CopyTo(stream);
            }

            return Ok("Documents uploaded successfully.");
        }



        [HttpGet("track")]
        public async Task<IActionResult> TrackLoan([FromQuery] string applicationNumber, [FromQuery] DateTime dob)
        {
            var application = await _loanRepo.TrackApplication(applicationNumber, dob);

            if (application == null)
                return NotFound("No application found with given details.");

            var response = new LoanStatusDTO
            {
                ApplicationNumber = application.ApplicationNumber,
                Status = application.Status,
                AppointmentDate = application.AppointmentDate,
                ApplicantName = $"{application.FirstName} {application.LastName}",
                Email = application.Email
            };

            return Ok(response);
        }



        [HttpPost("review")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ReviewApplication([FromBody] LoanStatusUpdateDTO request)
        {
            var application = await _context.LoanApplications.FindAsync(request.ApplicationId);
            if (application == null)
                return NotFound("Application not found.");

            application.Status = request.Status;
            application.Remarks = request.Remarks;

            await _context.SaveChangesAsync();
            return Ok("Loan application status updated.");
        }



        [HttpGet("my-applications")]
        [Authorize]
        public async Task<IActionResult> GetMyApplications()
        {
            var userIdClaim = User.FindFirst("id")?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized("User ID not found in token.");

            int userId = int.Parse(userIdClaim);

            var applications = await _loanRepo.GetApplicationsByUserIdAsync(userId);
            return Ok(applications);
        }




        [HttpGet("user/{userId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetApplicationsByUserId(int userId)
        {
            var applications = await _context.LoanApplications
                .Where(a => a.UserId == userId)
                .ToListAsync();

            if (applications == null || !applications.Any())
                return NotFound("No loan applications found for this user.");

            return Ok(applications);
        }



        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllApplications()
        {
            var applications = await _context.LoanApplications.ToListAsync();

            if (applications == null || applications.Count == 0)
                return NotFound("No loan applications found.");

            return Ok(applications);
        }



        [HttpDelete("{applicationId}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> DeleteApplication(int applicationId)
        {
            var userId = int.Parse(User.FindFirst("UserId").Value);
            var result = await _loanRepo.DeleteApplicationAsync(applicationId, userId);

            if (!result)
                return BadRequest("You can only delete your own application before it's reviewed.");

            return Ok("Application deleted successfully.");
        }



        [HttpPut("update-status/{applicationId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateLoanStatus(int applicationId, [FromBody] string newStatus)
        {
            if (newStatus != "Approved" && newStatus != "Rejected")
            {
                return BadRequest("Status must be either 'Approved' or 'Rejected'.");
            }

            var application = await _context.LoanApplications.FindAsync(applicationId);

            if (application == null)
            {
                return NotFound("Loan application not found.");
            }

            application.Status = newStatus;
            await _context.SaveChangesAsync();

            return Ok($"Loan application {applicationId} has been {newStatus.ToLower()}.");
        }



        [HttpGet("admin/get/{applicationId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetApplicationById(int applicationId)
        {
            var application = await _loanRepo.GetApplicationByIdAsync(applicationId);

            if (application == null)
            {
                return NotFound("Application not found.");
            }

            return Ok(application);
        }






    }
}
