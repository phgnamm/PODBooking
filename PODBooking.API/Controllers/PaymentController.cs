using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.Models.PaymentModels;
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
            _paymentGatewayService= paymentGatewayService;
        }
        [HttpPost]
        public IActionResult CreatePaymentUrl(PaymentInformationModel model)
        {
            var url = _paymentGatewayService.CreatePaymentUrlVnpay(model, HttpContext);

            return Ok(url);
        }
    }
}
