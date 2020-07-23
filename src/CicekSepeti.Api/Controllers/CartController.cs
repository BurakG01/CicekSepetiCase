using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CicekSepeti.Api.Domain.Models;
using CicekSepeti.Api.RequestModels;
using CicekSepeti.Api.ResponseModels;
using CicekSepeti.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CicekSepeti.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost("add")]
        public ActionResult<CartResponse> AddCart([FromBody] CartRequest cartRequest)
        {
            return _cartService.TryToAddCart(cartRequest);
        }
    }
}