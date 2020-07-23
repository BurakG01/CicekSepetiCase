using CicekSepeti.Api.Domain.InMemoryRepositories;
using CicekSepeti.Api.Domain.Models;
using Xunit;

namespace CicekSepetiUnitTests
{
    public class CartRepositoryTests
    {
        private readonly ICartRepository _cartRepository;
        public CartRepositoryTests()
        {
            _cartRepository = new CartRepository();
        }

        [Fact]
        public void Cart_Should_Be_Added()
        {
            var cart = new Cart(1, 1, 4);

            _cartRepository.InsertCard(cart);

            var insertedCart = _cartRepository.GetCart(cart.Id);

            Assert.Equal(insertedCart.Id, cart.Id);
        }
        [Fact]
        public void Cart_Should_Be_Updated()
        {
            var cart = new Cart(1, 1, 4);

            _cartRepository.InsertCard(cart);

            cart.Quantity = 12;

            _cartRepository.UpdateCard(cart);

            var updatedCart = _cartRepository.GetCart(cart.Id);

            Assert.Equal(updatedCart.Quantity, cart.Quantity);
        }
        [Fact]
        public void Cart_Should_Be_Return_When_Given_Cart_Id()
        {
            var cart = new Cart(1, 1, 4);

            _cartRepository.InsertCard(cart);

            var givenIdCart = _cartRepository.GetCart(1);

            Assert.Equal(givenIdCart.Id, cart.Id);
        }

        [Fact]
        public void Get_All_Carts_Should_Return_All_Carts()
        {
            var insertedCartCount = 3;
            for (int i = 0; i < insertedCartCount; i++)
            {
                var cart = new Cart(1, 1, 4);

                _cartRepository.InsertCard(cart);
            }
            var carts = _cartRepository.GetAllCarts();

            Assert.Equal(insertedCartCount, carts.Count);
        }
    }
}
