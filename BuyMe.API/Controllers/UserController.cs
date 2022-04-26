using BuyMe.API.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuyMe.API.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using BuyMe.API.Config;
using Microsoft.Extensions.Logging;

namespace BuyMe.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [LoggingFilter]
    public class UserController:ControllerBase
    {
        private IOptions<EmailConfig> _emailConfig;
        private ILogger<UserController> _logger;

        public UserController(IOptions<EmailConfig> emailConfig , ILogger<UserController> logger)
        {
            _emailConfig = emailConfig;
            _logger = logger;
        }
        
        [HttpGet("{userId}")]
        public IActionResult GetUserDetails([FromRoute]int userId)
        {

            _logger.LogInformation("Request originated");
            return Ok(_emailConfig.Value.FromEmailAccount);
        }

        [HttpPost]
        public IActionResult RegisterUser([FromBody]RegisterUserRequest data)
        {
            return Ok();
        }
        
    }
}
