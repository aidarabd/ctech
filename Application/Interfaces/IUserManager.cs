using Application.Models;
using Domain.Entities;

namespace Application.Interfaces;

public interface IUserManager
{
    Task<User> GetUser(LoginDto loginDto);
}