using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CicekSepeti.Api.Domain.Models;

namespace CicekSepeti.Api.Domain.InMemoryRepositories
{
    public interface ICartRepository
    {
        void InsertCard(Cart cart);
        void UpdateCard(Cart cart);
        Cart GetCart(int productId);
        List<Cart> GetAllCarts();
    }
}
