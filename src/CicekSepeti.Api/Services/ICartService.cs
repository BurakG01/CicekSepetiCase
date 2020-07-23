using CicekSepeti.Api.RequestModels;
using CicekSepeti.Api.ResponseModels;

namespace CicekSepeti.Api.Services
{
    public interface ICartService
    {
        CartResponse TryToAddCart(CartRequest cartRequest);
    }
}
