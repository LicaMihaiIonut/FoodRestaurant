
using FoodRestaurant.Abstractions.Aggregates.Users;
using FoodRestaurant.Domain.Aggregates.Users;

using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FoodRestaurant.Logic.Aggregates.Users;

public class UserTokenProvider : IUserTokenProvider
{
    private readonly string _key = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";

    public string Create(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_key);

        var tokenDesciptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(nameof(User.Name), user.Name ?? ""),
                new Claim(nameof(User.Email), user.Email ?? ""),
                new Claim(nameof(User.Phone), user.Phone ?? ""),
                new Claim(nameof(User.UserId), user.UserId.ToString()),
                new Claim(nameof(User.RestaurantId), user.RestaurantId?.ToString() ?? ""),
                new Claim(nameof(User.CartId), user.CartId?.ToString() ?? ""),
             }),
            Expires = DateTime.UtcNow.AddMonths(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDesciptor);

        return tokenHandler.WriteToken(token);
    }

    public int? Validate(string token)
    {
        try
        {
            if (token == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_key);

            var @params = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };

            tokenHandler.ValidateToken(token, @params, out var tokenOutput);
            var jwtToken = (JwtSecurityToken)tokenOutput;
            var stringUserId = jwtToken.Claims.FirstOrDefault(x => x.Type == nameof(User.UserId))?.Value;

            return int.Parse(stringUserId);
        }
        catch (Exception)
        {
            return null;
        }
    }
}
