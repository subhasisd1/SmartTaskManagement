using System.Net;

namespace SmartTaskManagement.Application.Exceptions;

public class NotFoundException : BaseException
{
    public NotFoundException(string message)
        : base(message, (int)HttpStatusCode.NotFound)
    {
    }
}