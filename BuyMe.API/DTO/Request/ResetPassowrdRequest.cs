using System.ComponentModel.DataAnnotations;

namespace BuyMe.API.DTO.Request
{
    public class ResetPassowrdRequest
    {
        public string Token { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
    }
}
