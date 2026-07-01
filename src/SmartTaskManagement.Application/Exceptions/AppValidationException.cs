using System.Net;

namespace SmartTaskManagement.Application.Exceptions;

public class AppValidationException : BaseException
{
    public AppValidationException(string message)
        : base(message, (int)HttpStatusCode.BadRequest)
    {
    }
}