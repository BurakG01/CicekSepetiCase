using System;
using CicekSepeti.Api.Domain.InMemoryRepositories;
using CicekSepeti.Api.Domain.Models;
using CicekSepeti.Api.Exceptions;
using CicekSepeti.Api.RequestModels;
using CicekSepeti.Api.ResponseModels;

namespace CicekSepeti.Api.Services
{
    public class CartService : ICartService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICartRepository _cartRepository;

        public CartService(IProductRepository productRepository, ICartRepository cartRepository)
        {
            _productRepository = productRepository;
            _cartRepository = cartRepository;
        }

        public CartResponse TryToAddCart(CartRequest cartRequest)
        {
            var product = _productRepository.GetProduct(cartRequest.ProductId);

            var cartResponseMessage = string.Empty;

            if (product == null)
            {
               throw  new ProductNotFoundException($"There is no product for this id :{cartRequest.ProductId}",
                   $"Bu ürün Id si ile ürün bulunamadı ÜrünId:{cartRequest.ProductId}");
            }

            if (cartRequest.Quantity > product.Stock)
            {
                throw new ProductStockExceedException($"The amount({cartRequest.Quantity}) you want to add is more than the stock amount({product.Stock}) of the product", $"Eklemek istediğiniz miktar({cartRequest.Quantity}) ürünün stok miktarından({product.Stock}) fazladır");
            }

            var cartInRepo = _cartRepository.GetCart(product.Id);
          
            if (cartInRepo != null)
            {
                cartInRepo.Quantity += cartRequest.Quantity;

                _cartRepository.UpdateCard(cartInRepo);

                cartResponseMessage = $"Cart Updated";

            }
            else
            {
                var newCart = new Cart(GenerateUniqueId(), cartRequest.ProductId, cartRequest.Quantity);

                _cartRepository.InsertCard(newCart);

                cartResponseMessage = $"Cart Inserted";
            } 

            product.Stock -= cartRequest.Quantity;

            _productRepository.UpdateProduct(product);

            var response = new CartResponse
            {
                Message = cartResponseMessage,
                AllCarts = _cartRepository.GetAllCarts()
            };

            return response;
        }

        private int GenerateUniqueId()
        {
            var now = DateTime.Now;
            var zeroDate = DateTime.MinValue.AddHours(now.Hour).AddMinutes(now.Minute).AddSeconds(now.Second).AddMilliseconds(now.Millisecond);
            int uniqueId = (int)(zeroDate.Ticks / 10000);
            return uniqueId;
        }
    }
}
