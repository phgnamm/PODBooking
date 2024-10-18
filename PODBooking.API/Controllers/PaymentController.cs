using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.Services;

namespace PODBooking.API.Controllers
{
    [Route("api/payments")]
    [ApiController]
    public class PaymentController : Controller
    {
        private readonly IPaymentGatewayService _paymentGatewayService;

        public PaymentController(IPaymentGatewayService paymentGatewayService)
        {
            _paymentGatewayService = paymentGatewayService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreatePaymentUrl([FromQuery] string bookingCode)
        {
            var url = await _paymentGatewayService.CreatePaymentUrlVnpay(bookingCode, HttpContext);
            return Ok(new { result = url });
        }

        [HttpPost("success")]
        public async Task<IActionResult> PaymentSuccess([FromQuery] string bookingCode)
        {
            await _paymentGatewayService.HandlePaymentSuccess(bookingCode);
            return Ok(new { message = "Payment completed successfully" });
        }
    }

}
