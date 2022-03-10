using System;

namespace WebApp.Api.Application.Exceptions
{
    public class BadRequestException: ApplicationException
    {
        public BadRequestException(string message): base(message)
        {

        }
    }
}
