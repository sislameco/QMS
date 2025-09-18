using Microsoft.IdentityModel.Tokens;
using Models.AppSettings;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Exceptions;

namespace Utils.JWT
{
    public class JwtTokenParse
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string OtpCode { get; set; }
        public string LoginId { get; set; }
    }
    public class UnAuthApi
    {
        public string Signin { get; set; }
        public string Forgotpassword { get; set; }
    }
    public static class JwtTokenDecode
    {
        public static JwtTokenParse GetTokenDecode(string token, bool isForPassworRecover = false)
        {
            try
            {
                if (token == null)
                {
                    if (isForPassworRecover)
                        throw new BadRequestException("Bad request.");
                    throw new UnAuthorizedException("Invalid Access Token");
                }


                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(AppSettings.Jwt.SecretKey);

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                string UserId = jwtToken.Claims.First(claim => claim.Type == "UserId").Value;
                string FirstName = jwtToken.Claims.First(claim => claim.Type == "FirstName").Value;
                string LoginId = jwtToken.Claims.First(claim => claim.Type == "LoginId").Value;

                return new JwtTokenParse
                {
                    UserId = UserId,
                    FirstName = FirstName,
                    LoginId = LoginId
                };
            }
            catch (Exception ex)
            {
                //Log.Information(ex.Message);
                if (isForPassworRecover)
                    throw new BadRequestException("Password recovery token expired. Please, Try again.");
                throw new UnAuthorizedException("Invalid Access Token");
            }
        }

    }
}
