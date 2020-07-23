namespace CicekSepeti.Api.Domain.Models
{
    public class Product
    {
        public Product(string productCode,int stock,int id)
        {
            ProductCode = productCode;
            Id = id;
            Stock = stock;
        }
        public string ProductCode { get; set; }
        public int Stock { get; set; }
        public int Id { get; set; }
    }
}
