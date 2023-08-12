
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Calendar.Models.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Calendar.API.AuthorizeJwt;

public static class AuthorizeJwt
{
    public static void AddAuthorizeJwt(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication()
            .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]!)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });
        services.AddAuthorization();
    }

    public static string GenerateToken(this IConfiguration configuration, int id, string name, string email)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Email, email),
                new Claim("userId", id.ToString())
            }),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);
        return tokenString;
    }

    public static int GetUserId(this HttpContext httpContext)
    {
        var claimsIdentity = httpContext.User.Identity as ClaimsIdentity;
        var userId = claimsIdentity.FindFirst("userId")?.Value;
        return int.Parse(userId);
    }
}
