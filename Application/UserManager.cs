using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using System.Security.Claims;
using System.Text;
using Infrastructure.Repositories;


namespace Application;

public class UserManager(IUserRepository userRepository, IMapper mapper): IUserManager
{
    private const int MaxAllowedAttempts = 5;
    private const int LockTimeInMinutes = 5;

    public async Task<User> GetUser(LoginDto loginDto)
    {
        var user = await userRepository.GetByUsername(loginDto.Username);

        if (user == null)
        {
            throw new UnauthorizedAccessException("No such user found");
        }

        if (user.LockoutEnd.HasValue && user.LockoutEnd.Value > DateTime.UtcNow)
        {
            throw new UnauthorizedAccessException("Account is locked. Please try again in 5 minutes");
        }

        if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
        {
            user.LoginAttempts++;

            if (user.LoginAttempts >= MaxAllowedAttempts)
            {
                user.LockoutEnd = DateTime.UtcNow.AddMinutes(LockTimeInMinutes);
                user.LoginAttempts = 0;
            }

            await userRepository.UpdateUser(user);
            await userRepository.SaveChangesAsync();

            throw new UnauthorizedAccessException("Invalid login or password");
        }

        user.LoginAttempts = 0;
        user.LockoutEnd = null;

        await userRepository.UpdateUser(user);
        await userRepository.SaveChangesAsync();

        return user;
    }

   
}