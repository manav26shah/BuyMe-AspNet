using BuyMe.API.Config;
using BuyMe.API.DTO.Request;
using BuyMe.API.DTO.Response;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BuyMe.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JWTConfig _jwtConfig;

        public LoginController(UserManager<IdentityUser> userManager, IOptionsMonitor<JWTConfig> jwtConfig)
        {
            _userManager = userManager;
            _jwtConfig = jwtConfig.CurrentValue;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(user.Email);
                if (existingUser != null)
                {
                    return BadRequest(new UserRegistrationResponse()
                    {
                        Error = new List<string>()
                    {
                        "Email already in use"
                    },
                        Success = false,

                    });
                }
                var newUser = new IdentityUser() { Email = user.Email, UserName = user.UserName  };

                var isCreated = await _userManager.CreateAsync(newUser, user.Password);
                if (isCreated.Succeeded)
                {
                    var jwtToken = GenerateJwtToken(newUser);
                    return Ok(new UserRegistrationResponse()
                    {
                        Success = true,
                        Token = jwtToken
                    });
                }
                else
                {
                    return BadRequest(new UserRegistrationResponse()
                    {
                        Error = isCreated.Errors.Select(x => x.Description).ToList(),
                        Success = false,

                    });
                }
            }
            else
            {
                return BadRequest(new UserRegistrationResponse()
                {
                    Error = new List<string>()
                    {
                        "Invalid Payload"
                    },
                    Success = false,

                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(user.Email);
                if (existingUser == null)
                {
                    return BadRequest(new UserRegistrationResponse()
                    {
                        Error = new List<string>()
                    {
                        "Invalid login Request"
                    },
                        Success = false,

                    });
                }
                var isCorrect = await _userManager.CheckPasswordAsync(existingUser, user.Password);
                if(!isCorrect)
                {
                    return BadRequest(new UserRegistrationResponse()
                    {
                        Error = new List<string>()
                    {
                        "Invalid login Request"
                    },
                        Success = false,

                    });
                }
                else
                {
                    var jwtToken = GenerateJwtToken(existingUser);
                    return Ok(new UserRegistrationResponse()
                    {
                        Success = true,
                        Token = jwtToken
                    });
                }
            }
            else
            {
                return BadRequest(new UserRegistrationResponse()
                {
                    Error = new List<string>()
                    {
                        "Invalid Payload"
                    },
                    Success = false,

                });
            }
        }

        [HttpPost]
        [Route("forgetPassword")]
        public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordRequest user)
        {
            if(string.IsNullOrEmpty(user.Email))
            {
                return BadRequest(new UserRegistrationResponse()
                {
                    Error = new List<string>()
                    {
                        "Invalid Payload"
                    },
                    Success = false,

                });
            }
            var _user = await _userManager.FindByEmailAsync(user.Email);
            if (_user == null)
            {
                return BadRequest(new UserRegistrationResponse()
                {
                    Error = new List<string>()
                    {
                        "User Email Not Found"
                    },
                    Success = false,

                });
            }
            if (user.NewPassword != user.ConfirmPassword)
            {
                return BadRequest(new UserRegistrationResponse()
                {
                    Error = new List<string>()
                        {
                            "New Password and Confirm Password Doesnot match"
                        },
                    Success = false,
                });
            }
            else
            {
                //var jwtToken = GenerateJwtToken(_user);
                var jwtToken = await _userManager.GeneratePasswordResetTokenAsync(_user);
                var result = await _userManager.ResetPasswordAsync(_user, jwtToken, user.NewPassword);
                if (result.Succeeded)
                {
                    return Ok(new UserRegistrationResponse()
                    {
                        Success = true,
                    });
                }
                return BadRequest(new UserRegistrationResponse()
                {
                    Error = new List<string>()
                    {
                        "Fail"
                    },
                    Success = false,

                });

            }
        }

        /*[HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("resetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPassowrdRequest data)
        {
            if(ModelState.IsValid)
            {
                var _user = await _userManager.FindByEmailAsync(data.Email);
                if(_user == null)
                {
                    return BadRequest(new UserRegistrationResponse()
                    {
                        Error = new List<string>()
                        {
                            "User Email Not Found"
                        },
                        Success = false,

                    });
                }
                if(data.NewPassword != data.ConfirmPassword)
                {
                    return BadRequest(new UserRegistrationResponse()
                    {
                        Error = new List<string>()
                        {
                            "New Password and Confirm Password Doesnot match"
                        },
                        Success = false,
                    });
                }

                var user = await _userManager.ResetPasswordAsync(_user, data.Token, data.NewPassword);
                if(user.Succeeded)
                {
                    return Ok(new UserRegistrationResponse()
                    {
                        Success = true,
                    }) ; 
                }
                return BadRequest(new UserRegistrationResponse()
                {
                    Error = new List<string>()
                    {
                        "Invalid Payload"
                    },
                    Success = false,

                });
            }
            else
            {
                return BadRequest(new UserRegistrationResponse()
                {
                    Error = new List<string>()
                    {
                        "Invalid Payload"
                    },
                    Success = false,

                });
            }
        }*/



        private string GenerateJwtToken(IdentityUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(4),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);
            return jwtToken;
        }

        /*public async string ForgetPassword(string email)
        {
            var _user = await _userManager.FindByEmailAsync(email);
            if (_user == null)
            {
                return new UserRegistrationResponse
                {
                    Error = new List<string>()
                    {
                        "Email ID Not Found"
                    },
                    Success = false,
                };
            }
            *//*var token = await _userManager.GeneratePasswordResetTokenAsync(_user);
            var encodedToken = Encoding.UTF8.GetBytes(token);
            var validToken = WebEncoders.Base64UrlEncode(encodedToken);
*//*
            var jwtToken = GenerateJwtToken(_user);

            return jwtToken;

        }*/
    }
}
