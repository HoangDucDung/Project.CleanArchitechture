using Project.Application.Contract.Models.Auths;
using Project.Application.Contract.Models.Auths.Logins;
using Project.Application.Contract.Models.Auths.Registers;
using Project.Application.Contract.Services.Auths;
using Project.Domain.Models.Auths;
using Project.Domain.Services.Auths;
using Project.Extensions.Extensions;
using Project.Host.Base.Lazyloads;
using Project.Libs.Exceptions;

namespace Project.Application.Services.Auths
{
    public class AuthService(ILazyloadProvider lazyloadProvider) : ApplicationServiceBase(lazyloadProvider), IAuthService
    {
        private ITokenManager tokenManager => lazyloadProvider.LazyGetRequiredService<ITokenManager>();

        public async Task<ResAuthenticationDto> LoginAsync(ReqUserLoginDto req, CancellationToken cancellationToken = default)
        {
            if(req == null)
            {
                throw new AuthException("Tham số không được để trống");
            }

            if(req.UserName.IsNullOrEmpty() || req.Password.IsNullOrEmpty())
            {
                throw new AuthException("Tên đăng nhập và mật khẩu không được để trống");
            }

            var reqToken = new ReqToken
            {
                UserId = Guid.NewGuid() //TODO: Lấy userId từ db
            };

            var res = tokenManager.GenerateToken(reqToken);

            //TODO: mapping sang ResAuthenticationDto
            return new ResAuthenticationDto
            {
                UserId = res.UserId,
                AccessToken = res.AccessToken,
                RefreshToken = res.RefreshToken
            };
        }

        public Task<string> RegisterAsync(ReqRegisterDto req, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}