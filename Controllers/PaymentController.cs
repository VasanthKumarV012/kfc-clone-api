using FullStackApi.Models;
using Microsoft.AspNetCore.Mvc;
using Razorpay.Api;

namespace FullStackApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public PaymentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("create-order")]
        public IActionResult CreateOrder(CreateOrderRequest request)
        {
            var key = _configuration["Razorpay:Key"];
            var secret = _configuration["Razorpay:Secret"];

            RazorpayClient client = new RazorpayClient(key, secret);

            Dictionary<string, object> options = new Dictionary<string, object>();

            options.Add("amount", request.Amount * 100);

            options.Add("currency", "INR");

            options.Add("receipt", Guid.NewGuid().ToString());

            Razorpay.Api.Order order = client.Order.Create(options);

            return Ok(new
            {
                orderId = order["id"].ToString(),
                amount = request.Amount,
                key = key
            });
        }
    }
}