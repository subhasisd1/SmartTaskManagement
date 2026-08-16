using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartTaskManagement.Application.DTOs.Payment;
using SmartTaskManagement.Application.Interfaces;
using System.Security.Claims;

namespace SmartTaskManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(
            IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("pay")]
        [Authorize]
        public async Task<IActionResult> CreatePayment(
    CreatePaymentDto model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var userId = User.FindFirst("sub")?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var payment = await _paymentService.CreatePaymentAsync(
                model.OrderId,
                userId,
                model.Amount,
                model.PaymentMethod);

            return Ok(payment);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPayment(int id)
        {
            var payment =
                await _paymentService.GetPaymentAsync(id);

            if (payment == null)
                return NotFound();

            return Ok(payment);
        }
    }
}
