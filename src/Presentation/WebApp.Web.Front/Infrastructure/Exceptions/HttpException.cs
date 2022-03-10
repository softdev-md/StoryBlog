using System;
using System.Net;

namespace WebApp.Web.Front.Infrastructure.Exceptions
{
    public class HttpException : Exception
    {
        private readonly int _httpStatusCode;

        public HttpException(string message)
            : base(message)
        {
            _httpStatusCode = 400;
        }

        public HttpException(int httpStatusCode) => _httpStatusCode = httpStatusCode;

        public HttpException(HttpStatusCode httpStatusCode) => _httpStatusCode = (int)httpStatusCode;

        public HttpException(int httpStatusCode, string message)
            : base(message)
        {
            _httpStatusCode = httpStatusCode;
        }

        public HttpException(HttpStatusCode httpStatusCode, string message)
            : base(message)
        {
            _httpStatusCode = (int)httpStatusCode;
        }

        public HttpException(int httpStatusCode, string message, Exception inner)
            : base(message, inner)
        {
            _httpStatusCode = httpStatusCode;
        }

        public HttpException(HttpStatusCode httpStatusCode, string message, Exception inner)
            : base(message, inner)
        {
            _httpStatusCode = (int)httpStatusCode;
        }

        public int StatusCode => _httpStatusCode;
    }
}
