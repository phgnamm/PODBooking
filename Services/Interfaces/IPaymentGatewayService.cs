using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IPaymentGatewayService
    {
        //Task<string> CreatePaymentUrlVnpay(PaymentInformationModel request, HttpContext httpContext);
        //Task HandlePaymentSuccess(string code);
        Task<string> CreatePaymentUrlVnpay(string bookingCode, HttpContext httpContext);
        Task HandlePaymentSuccess(string bookingCode);
    }
}
