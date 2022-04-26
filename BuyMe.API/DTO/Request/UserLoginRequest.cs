﻿using System.ComponentModel.DataAnnotations;

namespace BuyMe.API.DTO.Request
{
    public class UserLoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
