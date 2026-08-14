using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartTaskManagement.Application.DTOs.Payment;
using SmartTaskManagement.Application.Interfaces;

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

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreatePayment(
    CreatePaymentDto model)
        {
            foreach (var claim in User.Claims)
            {
                Console.WriteLine($"Type: {claim.Type}, Value: {claim.Value}");
            }
            var userId = User.FindFirst("sub")?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var payment = await _paymentService.CreatePaymentAsync(
                model.OrderId,
                userId,
                model.Amount);

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
