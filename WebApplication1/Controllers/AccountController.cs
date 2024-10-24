using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController(IUserManager userManager, IConfiguration configuration, TokenBlacklistService tokenBlacklistService):ControllerBase
{
    private const int TokenExpirationTime = 1;

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var user = await userManager.GetUser(loginDto);

        var token = GenerateJwtToken(user);

        return Ok(new { token });
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        var authHeader = Request.Headers["Authorization"].FirstOrDefault();

        if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
        {
            return BadRequest("Authorization token is missing ");
        }

        // Get the token (remove "Bearer " prefix)
        var token = authHeader.Substring("Bearer ".Length).Trim();

        if (string.IsNullOrEmpty(token))
        {
            return BadRequest("Token is missing.");
        }

        // Here, you would proceed to blacklist the token
        var jwtTokenHandler = new JwtSecurityTokenHandler();
        var securityToken = jwtTokenHandler.ReadToken(token) as JwtSecurityToken;
        var expiryDate = securityToken?.ValidTo ?? DateTime.UtcNow.AddHours(TokenExpirationTime);

        // Add the token to the blacklist (assuming _tokenBlacklistService is injected)
        tokenBlacklistService.BlacklistToken(token, expiryDate);

        return Ok("Logged out successfully.");
    }

    private string GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(configuration["jwt:secret_key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, user.Id.ToString()) }),
            Expires = DateTime.UtcNow.AddHours(TokenExpirationTime),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}