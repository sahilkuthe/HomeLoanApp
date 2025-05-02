using HomeLoanAPI.DTOs;
using HomeLoanAPI.Models;
using HomeLoanAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomeLoanAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanApplicationController : ControllerBase
    {
        private readonly ILoanRepository _loanRepo;

        public LoanApplicationController(ILoanRepository loanRepo)
        {
            _loanRepo = loanRepo;
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

        [HttpPost("upload-documents/{applicationNumber}")]
        public async Task<IActionResult> UploadDocuments(string applicationNumber, [FromForm] IFormFile panCard,
            [FromForm] IFormFile voterId, [FromForm] IFormFile salarySlip,
            [FromForm] IFormFile loa, [FromForm] IFormFile noc, [FromForm] IFormFile agreement)
        {
            var application = await _loanRepo.GetByApplicationNumber(applicationNumber);

            if (application == null)
                return NotFound("Application not found.");

            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            string SaveFile(IFormFile file, string label)
            {
                var ext = Path.GetExtension(file.FileName);
                var fileName = $"{applicationNumber}_{label}{ext}";
                var filePath = Path.Combine(folderPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                return $"/uploads/{fileName}";
            }

            application.PanCardPath = SaveFile(panCard, "PAN");
            application.VoterIdPath = SaveFile(voterId, "VOTER");
            application.SalarySlipPath = SaveFile(salarySlip, "SALARY");
            application.LOAPath = SaveFile(loa, "LOA");
            application.NOCPath = SaveFile(noc, "NOC");
            application.AgreementPath = SaveFile(agreement, "AGREEMENT");

            await _loanRepo.SubmitApplication(application); // reuse method to update

            return Ok(new { Message = "Documents uploaded successfully." });
        }

    }
}
