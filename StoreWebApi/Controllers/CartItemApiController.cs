using BL;
using Microsoft.AspNetCore.Mvc;
using Repositories;

using System.Net;

namespace StoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemApiController : ControllerBase
    {
        protected APIResponce _response;
        private ICartItemRepository _cartItemRepository;

        public CartItemApiController(ICartItemRepository cartItemRepository)
        {
            _cartItemRepository = cartItemRepository;
            _response = new();
        }
        [HttpGet("GetCartItems/{cartId:Guid}", Name = "GetCartItems")]
        public async Task<ActionResult<APIResponce>> GetCartItems(Guid cartId)
        {
            var cartItems = await _cartItemRepository.GetAllItemsAsync((c=>c.CartId== cartId),includeProps:"Product");
            _response.Result = cartItems.Select(p => new CartViewModel(p));
            _response.StatusCode = System.Net.HttpStatusCode.OK;

            return Ok(_response);
        }

        [HttpGet("GetCartItem/{id:Guid}", Name = "GetCartItem")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<APIResponce>> GetCartItem(Guid id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var cart = await _cartItemRepository.GetItemAsync(p => p.Id == id, includeProps: "Product");
            if (cart == null)
            {
                return NotFound();
            }
            _response.Result = new CartViewModel(cart);
            _response.StatusCode = System.Net.HttpStatusCode.OK;

            return Ok(_response);
        }
        [HttpPost(Name = "CreateCartItem")]
        public async Task<ActionResult<APIResponce>> CreateCartItem([FromBody] CartViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model == null)
            {
                return BadRequest(model);
            }
            await _cartItemRepository.AddItemAsync(model);

            _response.Result = model;
            _response.StatusCode = System.Net.HttpStatusCode.Created;

            return CreatedAtRoute("GetCartItem", new { id = model.Id }, _response);
        }

        [HttpDelete("{id:Guid}", Name = "DeleteCartItem")]
        public async Task<ActionResult<APIResponce>> DeleteCartItem(Guid id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var item = await _cartItemRepository.GetItemAsync(p => p.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            await _cartItemRepository.DeleteItemAsync(id);
            _response.StatusCode = HttpStatusCode.NoContent;
            _response.IsSuccess = true;
            return Ok(_response);
        }
        [HttpPut("{id:Guid}", Name = "UpdateCartItem")]
        public IActionResult UpdateCartItem(Guid id, [FromBody]CartViewModel model)
        {
            if (model == null || id != model.Id)
            {
                return BadRequest();
            }
            _cartItemRepository.ChangeItemAsync(model);
            _response.StatusCode = HttpStatusCode.NoContent;
            _response.IsSuccess = true;
            return Ok(_response);


        }

    }
}
