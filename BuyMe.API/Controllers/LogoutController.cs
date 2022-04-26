using BuyMe.BL.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BuyMe.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogoutController : ControllerBase
    {
        private ICartService _cartService;
        private ILogger<CartController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IProductService _productService;
        private readonly IOrderService _order;

        public LogoutController(ICartService cartService, ILogger<CartController> logger, UserManager<IdentityUser> userManager, IProductService productService, IOrderService order)
        {
            _cartService = cartService;
            _logger = logger;
            _userManager = userManager;
            _productService = productService;
            _order = order;
        }

        [HttpGet]
        public IActionResult Logout()
        {
            //create new Order
            //Copy info of cart into it 
            //removeCart
            //removeToken
            
            return Ok();
        }
    }
}
