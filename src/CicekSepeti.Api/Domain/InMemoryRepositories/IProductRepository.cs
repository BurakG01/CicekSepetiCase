using CicekSepeti.Api.Domain.Models;

namespace CicekSepeti.Api.Domain.InMemoryRepositories
{
    public interface IProductRepository
    {
        public Product GetProduct(int productId);
        void UpdateProduct(Product product);
    }

}
