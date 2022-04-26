using System.ComponentModel.DataAnnotations;

namespace BuyMe.API.DTO.Request
{
    public class UserRegistrationRequest
    {
        [Required]
        public string UserName { get; set; } 
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        
       /* public string FirstName { get; set; }
   
        public string LastName { get; set; }
    
        public string MobileNumber { get; set; }*/
    }
}
