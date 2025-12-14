using Project.Domain.Models.Auths;

namespace Project.Domain.Services.Auths
{
    public interface ITokenManager
    {
        /// <summary>
        /// Hàm tạo token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        ResToken GenerateToken(ReqToken token);

        /// <summary>
        /// Tạo refresh token
        /// </summary>
        /// <returns></returns>
        string GenerateRefreshToken();
    }
}