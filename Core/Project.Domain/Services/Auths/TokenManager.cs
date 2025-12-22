using Microsoft.IdentityModel.Tokens;
using Project.Domain.Models.Auths;
using Project.Domain.Share.Constants;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Project.Host.Base.Lazyloads;
using Project.Host.Base.Configs;

namespace Project.Domain.Services.Auths
{
    public class TokenManager(ILazyloadProvider lazyloadProvider) : ManagerServiceBase(lazyloadProvider), ITokenManager
    {
        private IOptions<AuthConfig> _authConfig => lazyloadProvider.GetRequiredService<IOptions<AuthConfig>>();

        public ResToken GenerateToken(ReqToken param)
        {
            var claims = new[]
            {
                new Claim(ClaimToken.UserId, Guid.NewGuid().ToString()),
                //TODO: Add more claims here
            };

            var jwtToken = BuildToken(claims);

            return new ResToken
            {
                UserId = param.UserId,
                AccessToken = jwtToken,
                RefreshToken = GenerateRefreshToken()
            };
        }


        /// <summary>
        /// TODO: Tạo refresh token
        /// </summary>
        /// <returns></returns>
        public string GenerateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Hàm xây dựng token
        /// </summary>
        /// <param name="claims"></param>
        /// <returns></returns>
        private string BuildToken(Claim[] claims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authConfig.Value.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(30);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(_authConfig.Value.ExpiresTime),
                Issuer = _authConfig.Value.Issuer,
                Audience = _authConfig.Value.Audience,
                SigningCredentials = creds
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);

            return jwtToken;
        }
    }
}