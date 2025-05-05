using System.ComponentModel.DataAnnotations;

namespace HomeLoanAPI.DTOs
{
    public class DocumentUploadDTO
    {
        public string ApplicationNumber { get; set; }

        [Required]
        public IFormFile PanCard { get; set; }

        [Required]
        public IFormFile AadharCard { get; set; }

        [Required]
        public IFormFile SalarySlip { get; set; }
    }
}
