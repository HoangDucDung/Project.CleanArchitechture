namespace Project.Application.Contract.Models.Auths.Logins
{
    public class ReqUserLoginDto
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
