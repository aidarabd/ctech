using System.Net;
using Application.Enum;
using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Persistence;

namespace Application;

public class PaymentManager(IUserRepository userRepository, IPaymentRepository paymentRepository, IDbContext context) : IPaymentManager
{
    public async Task<DataResponse> ProcessPayment(int userId)
    {
        var response = new DataResponse();
        await using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            var user = await userRepository.GetById(userId);
            
            if (user == null)
            {
                response.Message = "User not found.";
                response.StatusCode = (int)HttpStatusCode.NoContent;
                return response;
            }

            if (user.Balance < 1.1m)
            {
                response.Message = "Insufficient balance";
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                return response;
            }

            user.Balance -= 1.1m;

            var payment = new Payment
            {
                UserId = userId,
                Amount = 1.1m,
                OperationDate = DateTime.UtcNow,
                OperationTypeId = (int) OPERATION_TYPE.OUT
            };

            await userRepository.UpdateUser(user);
            paymentRepository.AddPayment(payment);

            await context.SaveChangesAsync();

            await transaction.CommitAsync();
            response.Message = "Payment successful";
            return response;
        }
        catch(Exception e)
        {
            await transaction.RollbackAsync();
            response.Message = "Payment failed";
            return response;
        }
    }
}