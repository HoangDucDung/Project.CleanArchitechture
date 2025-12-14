using Project.Application.Contract.Models.Auths;
using Project.Application.Contract.Models.Auths.Logins;
using Project.Application.Contract.Models.Auths.Registers;

namespace Project.Application.Contract.Services.Auths
{
    public interface IAuthService
    {
        /// <summary>
        /// Đăng nhập
        /// </summary>
        /// <param name="req"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResAuthenticationDto> LoginAsync(ReqUserLoginDto req, CancellationToken cancellationToken = default);

        /// <summary>
        /// Đăng ký
        /// </summary>
        /// <param name="req"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string> RegisterAsync(ReqRegisterDto req, CancellationToken cancellationToken = default);
    }
}
