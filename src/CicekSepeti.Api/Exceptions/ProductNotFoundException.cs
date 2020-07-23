
namespace CicekSepeti.Api.Exceptions
{
    public class ProductNotFoundException:BaseCustomException
    {
        public ProductNotFoundException(string message, string description) : base(message, description,(int)System.Net.HttpStatusCode.NotFound)
        {
        }
    }
}
