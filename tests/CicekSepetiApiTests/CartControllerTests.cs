using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CicekSepeti.Api;
using CicekSepeti.Api.RequestModels;
using CicekSepeti.Api.ResponseModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Xunit;

namespace CicekSepetiApiTests
{
    public class CartControllerTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;
        public CartControllerTests()
        {
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            _client = _server.CreateClient();
        }
        [Fact]
        public async Task Cart_Add_Api_Should_Return_Not_Found_Status_Code_When_Product_Not_Found()
        {
            var cartRequest= new CartRequest
            {
                ProductId = 23,
                Quantity = 10
            };

            var content = new StringContent(JsonConvert.SerializeObject(cartRequest), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/cart/add",content);

            var expectedStatusCode = HttpStatusCode.NotFound;

            Assert.Equal(expectedStatusCode,response.StatusCode);
        }

        [Fact]
        public async Task Cart_Add_Api_Should_Return_Bad_Request_Status_Code_When_Quantity_Greater_Then_Product_Stock()
        {
            var cartRequest = new CartRequest
            {
                ProductId = 2,
                Quantity = 31
            };

            var content = new StringContent(JsonConvert.SerializeObject(cartRequest), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/cart/add", content);

            var expectedStatusCode = HttpStatusCode.BadRequest;

            Assert.Equal(expectedStatusCode, response.StatusCode);
        }

        [Fact]
        public async Task Cart_Add_Api_Should_Return_Inserted_Cart_Response_When_Cart_Request()
        {
            var cartRequest = new CartRequest
            {
                ProductId = 2,
                Quantity = 5
            };

            var content = new StringContent(JsonConvert.SerializeObject(cartRequest), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/cart/add", content);
            var result = await response.Content.ReadAsStringAsync();

           var cartResponseMessage= JsonConvert.DeserializeObject<CartResponse>(result).Message;

            var expectedStatusCode = HttpStatusCode.OK;
            var expectedCartResponseMessage = "Cart Inserted";

            Assert.Equal(expectedStatusCode, response.StatusCode);

            Assert.Equal(expectedCartResponseMessage,cartResponseMessage);

        }

        [Fact]
        public async Task Cart_Add_Api_Should_Return_Updated_Cart_Response_When_Same_Cart_Request()
        {
            var firstCartRequest = new CartRequest
            {
                ProductId = 2,
                Quantity = 5
            };
            var secondCartRequest= new CartRequest
            {
                ProductId = 2,
                Quantity = 10
            };

            var firstContent = new StringContent(JsonConvert.SerializeObject(firstCartRequest), Encoding.UTF8, "application/json");

            var secondContent = new StringContent(JsonConvert.SerializeObject(secondCartRequest), Encoding.UTF8, "application/json");

            await _client.PostAsync("/api/cart/add", firstContent);

           var response=await _client.PostAsync("/api/cart/add", secondContent);

            var result = await response.Content.ReadAsStringAsync();

            var expectedQuantity = firstCartRequest.Quantity + secondCartRequest.Quantity;

            var cartResponseMessage = JsonConvert.DeserializeObject<CartResponse>(result);

            var expectedStatusCode = HttpStatusCode.OK;
            var expectedCartResponseMessage = "Cart Updated";

            Assert.Equal(expectedStatusCode, response.StatusCode);
            Assert.Equal(expectedCartResponseMessage, cartResponseMessage.Message);
            Assert.Equal(expectedQuantity,cartResponseMessage.AllCarts.First().Quantity);

        }
    }
}
