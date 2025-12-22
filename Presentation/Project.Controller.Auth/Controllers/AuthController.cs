using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Contract.Models.Auths;
using Project.Application.Contract.Models.Auths.Logins;
using Project.Application.Contract.Services.Auths;
using Project.Controller.Base.Controller;
using Project.Host.Base.Lazyloads;

namespace Project.Controller.Auth.Controllers
{
    [AllowAnonymous]
    public class AuthController(ILazyloadProvider lazyloadProvider) : BaseController(lazyloadProvider)
    {
        private IAuthService _authService => _lazyloadProvider.GetRequiredService<IAuthService>();

        /// <summary>
        /// Đăng nhập
        /// </summary>
        /// <param name="param">GuiId Data</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public async Task<ResAuthenticationDto> LoginAsync([FromBody] ReqUserLoginDto param, CancellationToken cancellationToken = default)
        {
            var res = await _authService.LoginAsync(param, cancellationToken);
            return res;
        }
    }
}