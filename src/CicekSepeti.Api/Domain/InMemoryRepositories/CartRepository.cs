using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CicekSepeti.Api.Domain.Models;

namespace CicekSepeti.Api.Domain.InMemoryRepositories
{
    public class CartRepository: ICartRepository
    {
        private readonly List<Cart> _carts;
        public CartRepository()
        {
            _carts=new List<Cart>();
        }
        public void InsertCard(Cart cart)
        {
           _carts.Add(cart);
        }
        public void UpdateCard(Cart cart)
        {
            var index = _carts.FindIndex(x => x.Id == cart.Id);
            if (index != -1)
            {
                _carts[index] = cart;
            }
        }
        public Cart GetCart(int productId)
        {
            return _carts.FirstOrDefault(x => x.ProductId == productId);
        }
        public List<Cart> GetAllCarts()
        {
            return _carts;
        }
    }
}
