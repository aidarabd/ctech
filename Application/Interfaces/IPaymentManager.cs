using Application.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Application.Interfaces;

public interface IPaymentManager
{
    Task<DataResponse> ProcessPayment(int userId);
}