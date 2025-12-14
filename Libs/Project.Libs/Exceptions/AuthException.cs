using System.Net;

namespace Project.Libs.Exceptions
{
    public class AuthException : BaseHttpStatusCodeException
    {
        public AuthException(string message) : base(message)
        {
            StatusCode = HttpStatusCode.Unauthorized;
        }

        public override HttpStatusCode StatusCode { get; set; } = HttpStatusCode.Unauthorized;
    }
}
