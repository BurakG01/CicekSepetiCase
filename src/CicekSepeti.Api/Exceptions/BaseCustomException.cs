using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CicekSepeti.Api.Exceptions
{
    public class BaseCustomException : Exception
    {
        public int HttpStatusCode { get; }

        public string Description { get; }

        public BaseCustomException(string message, string description, int httpStatusCode) : base(message)
        {
            HttpStatusCode = httpStatusCode;
            Description = description;
        }
    }
}
