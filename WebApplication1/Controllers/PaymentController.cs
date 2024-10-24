using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PaymentController(IPaymentManager paymentManager) : ControllerBase
{
    
    [HttpPost("pay")]
    public async Task<DataResponse> MakePayment()
    {
        var userId = int.Parse(User.Identity.Name);
        var response = await paymentManager.ProcessPayment(userId);

        return response;
    }
}