using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Common;

public static class Jwt
{
    public static Guid GetUserId(this string jwt)
    {
        var claims = jwt.GetClaims();
        
        return Guid.Parse(claims.First(t => t.Type.ToString() == ClaimTypes.Id.ToString()).Value);
    }

    public static IEnumerable<Claim> GetClaims(this string jwt)
    {
        return new JwtSecurityTokenHandler().ReadJwtToken(jwt.Split(" ")[1]).Claims;
    }
}