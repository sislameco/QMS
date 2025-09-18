using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models.AppSettings;
using Models.Entities.UserManagement;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Utils
{
    public interface IJwtGenerator
    {
        public string GenerateJWT(UserModel userInfo,
                                    string ipAddress,
                                    long loginId,
                                    int expireTime = 480);
    }
    public class JwtGenerator : IJwtGenerator
    {
        public JwtGenerator()
        {
        }
        public string GenerateJWT(UserModel userInfo,
                                    string ipAddress,
                                    long loginId,
                                    int expireTime = 480)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettings.Jwt.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserName),
                new Claim("FirstName", userInfo.FirstName),
                new Claim("UserId", userInfo.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("IpAddress", ipAddress),
                new Claim("LoginId", loginId.ToString())
            };
            var token = new JwtSecurityToken(
            issuer: AppSettings.Jwt.Issuer,
            audience: AppSettings.Jwt.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(expireTime),
            signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}