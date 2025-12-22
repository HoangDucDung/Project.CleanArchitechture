using System.Net;

namespace Project.Libs.Exceptions
{
    public class BusinessException : BaseHttpStatusCodeException
    {
        public BusinessException(string message, HttpStatusCode httpStatus = HttpStatusCode.UnprocessableContent) : base(message) => StatusCode = httpStatus;

        public override HttpStatusCode StatusCode { get; set; } = HttpStatusCode.UnprocessableContent;
    }
}
