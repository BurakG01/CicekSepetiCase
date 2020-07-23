using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CicekSepeti.Api.Exceptions
{
    public class ProductStockExceedException:BaseCustomException
    {
        public ProductStockExceedException(string message, string description) : base(message, description, (int)System.Net.HttpStatusCode.BadRequest)
        {
        }
    }
}
