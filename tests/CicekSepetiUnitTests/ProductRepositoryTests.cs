using CicekSepeti.Api.Domain.InMemoryRepositories;
using CicekSepeti.Api.Domain.Models;
using Xunit;

namespace CicekSepetiUnitTests
{
    public class ProductRepositoryTests
    {
        private readonly IProductRepository _productRepository;
        public ProductRepositoryTests()
        {
            _productRepository = new ProductRepository();
        }

        [Fact]
        public void Get_Products_Should_Return_Product_When_Given_Id()
        {
            var product = _productRepository.GetProduct(1);

            Assert.Equal("P11", product.ProductCode);
        }
        [Fact]
        public void Product_Stock_Should_Be_Updated()
        {
            var product = new Product("P11", 22, 1);

            _productRepository.UpdateProduct(product);

            var updatedProduct = _productRepository.GetProduct(1);

            Assert.Equal(product.Stock, updatedProduct.Stock);

        }
    }
}
