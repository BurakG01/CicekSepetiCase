using System.Collections.Generic;
using System.Linq;
using CicekSepeti.Api.Domain.Models;

namespace CicekSepeti.Api.Domain.InMemoryRepositories
{
    public class ProductRepository: IProductRepository
    {
        private readonly List<Product> _products;

        public ProductRepository()
        {
            _products = new List<Product>()
            {
                new Product("P11",50,1),
                new Product("P12",30,2),
                new Product("P12",60,3)
            };
        }

        public Product GetProduct(int productId)
        {
            return _products.FirstOrDefault(x => x.Id == productId);
        }
        public void UpdateProduct(Product product)
        {
            var index = _products.FindIndex(x => x.Id == product.Id);
            if (index != -1)
            {
                _products[index] = product;
            }
        }
    }
}
