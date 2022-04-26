using BuyMe.BL;
using BuyMe.BL.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BuyMe.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private ICartService _cartService;
        private ILogger<CartController> _logger;
        private readonly UserManager<IdentityUser> _userManager;

        public CartController(ICartService cartService, ILogger<CartController> logger, UserManager<IdentityUser> userManager)
        {
            _cartService = cartService;
            _logger = logger;
            _userManager = userManager;
        }

        [HttpPost("{_productId}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> AddToCart([FromRoute] int _productId)
        {
            try
            {
                var userId = _userManager.GetUserId(HttpContext.User);
                var newCart = new CartBL
                {
                    ProductId = _productId,
                    Email = userId, 
                };
                var result = await _cartService.AddToCart(newCart);
                _logger.LogTrace("Connected and sent data to the DB correctly");
                if (result)
                {
                    return StatusCode(StatusCodes.Status201Created);

                }
                else
                {
                    return BadRequest("Error while adding new product");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        
        [HttpPatch("{_productId}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdateToCart([FromRoute] int _productId)
        {
            try
            {
                var userId = _userManager.GetUserId(HttpContext.User);
                var newCart = new CartBL
                {
                    ProductId = _productId,
                    Email = userId,
                };
                var result = await _cartService.UpdateToCart(newCart);
                _logger.LogTrace("Connected and sent data to the DB correctly");
                if (result)
                {
                    return StatusCode(StatusCodes.Status201Created);

                }
                else
                {
                    return BadRequest("Error while adding new product");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{_productId}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteFromCart([FromRoute] int _productId)
        {
            try
            {
                var userId = _userManager.GetUserId(HttpContext.User);
                /*var newCart = new CartBL
                {
                    ProductId = _productId,
                    Email = userId,
                };*/
                var result = await _cartService.DeleteFromCart(_productId);
                _logger.LogTrace("Connected and sent data to the DB correctly");
                if (result)
                {
                    return StatusCode(StatusCodes.Status201Created);

                }
                else
                {
                    return BadRequest("Error while adding new product");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
