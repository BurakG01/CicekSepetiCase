
namespace CicekSepeti.Api.Domain.Models
{
    public class Cart
    {
        public Cart(int id,int productId,int quantity)
        {
            Id = id;
            ProductId = productId;
            Quantity = quantity;
        }
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
