using System.ComponentModel.DataAnnotations;

namespace BuyMe.API.DTO.Request
{
    public class ForgetPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
