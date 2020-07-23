using System.Collections.Generic;
using CicekSepeti.Api.Domain.Models;

namespace CicekSepeti.Api.ResponseModels
{
    public class CartResponse
    {
        public  string Message { get; set; }
        public List<Cart> AllCarts { get; set; }
    }
}
