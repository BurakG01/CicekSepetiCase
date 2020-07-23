using CicekSepeti.Api.Domain.InMemoryRepositories;
using CicekSepeti.Api.Exceptions;
using CicekSepeti.Api.RequestModels;
using CicekSepeti.Api.Services;
using Xunit;

namespace CicekSepetiUnitTests
{
    public class CartServiceTests
    {
        private readonly ICartService _cartService;
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        public CartServiceTests()
        {
            _cartRepository = new CartRepository();
            _productRepository = new ProductRepository();
            _cartService = new CartService(_productRepository, _cartRepository);
        }

        [Fact]
        public void Cart_Service_Should_Throw_Exception_When_Product_Not_Found()
        {
            var cartRequest = new CartRequest
            {
                ProductId = 12,
                Quantity = 33
            };

            Assert.Throws<ProductNotFoundException>(() => _cartService.TryToAddCart(cartRequest));
        }

        [Fact]
        public void Cart_Service_Should_Throw_Exception_When_Quantity_Greater_Then_Product_Stock()
        {
            var cartRequest = new CartRequest
            {
                ProductId = 1,
                Quantity = 51
            };

            Assert.Throws<ProductStockExceedException>(() => _cartService.TryToAddCart(cartRequest));
        }

        [Fact]
        public void Product_Stock_Should_Decrease_When_Cart_Inserted()
        {
            var cartRequest = new CartRequest
            {
                ProductId = 1,
                Quantity = 22
            };
            var productStock = _productRepository.GetProduct(cartRequest.ProductId).Stock;
            var expectedStock = productStock - cartRequest.Quantity;

            _cartService.TryToAddCart(cartRequest);

            var decreasedProductStock = _productRepository.GetProduct(cartRequest.ProductId).Stock;

            Assert.Equal(expectedStock, decreasedProductStock);
        }

        [Fact]
        public void Cart_Quantity_Should_Increase_When_Same_Cart_Request()
        {
            var productId = 1;
            var firstCartRequest = new CartRequest
            {
                ProductId = productId,
                Quantity = 10
            };
            var secondCartRequest = new CartRequest
            {
                ProductId = productId,
                Quantity = 10
            };
            _cartService.TryToAddCart(firstCartRequest);

            _cartService.TryToAddCart(secondCartRequest);

            var expectedCartQuantity = firstCartRequest.Quantity + secondCartRequest.Quantity;

            var updatedCart = _cartRepository.GetCart(productId);

            Assert.Equal(expectedCartQuantity, updatedCart.Quantity);

        }

    }
}
