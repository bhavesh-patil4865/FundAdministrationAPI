using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FundAdministration.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/auth")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public AuthController(
        IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost("login")]
    public IActionResult Login()
    {
        var claims = new[]
        {
            new Claim(
                ClaimTypes.Name,
                "Admin")
        };

        var key =
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _configuration["Jwt:Key"]!));

        var creds =
            new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

        var token =
            new JwtSecurityToken(
                issuer:
                    _configuration["Jwt:Issuer"],

                audience:
                    _configuration["Jwt:Audience"],

                claims: claims,

                expires:
                    DateTime.Now.AddHours(1),

                signingCredentials: creds
            );

        return Ok(new
        {
            Token =
                new JwtSecurityTokenHandler()
                .WriteToken(token)
        });
    }
}