using BuyMe.API.DTO.Response;
using BuyMe.BL.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Security.Claims;

namespace BuyMe.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IOrderService _order;
        private readonly ILogger<OrderController> _logger;
        private readonly UserManager<IdentityUser> _userManager;

        public OrderController(IProductService productService, ILogger<OrderController> logger, UserManager<IdentityUser> userManager, IOrderService order)
        {
            _productService = productService;
            _logger = logger;
            _userManager = userManager;
            _order = order;
        }

        /// <summary>
        /// API to Get all Orders
        /// </summary>
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetOrders()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var dbProducts = _order.GetOrders(userId);
            var retProducts = new List<OrderResponse>();

            foreach (var item in dbProducts)
            {
                retProducts.Add(new OrderResponse
                {
                    Id = item.Id,
                    ProductId = item.ProductId,
                }); 
            }  // Auto mapper
            return Ok(retProducts);
        }

      
    }
}
