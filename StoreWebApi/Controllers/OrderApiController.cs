using BL;
using Microsoft.AspNetCore.Mvc;
using Repositories;

using System.Net;

namespace StoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderApiController : ControllerBase
    {
        protected APIResponce _response;
        private IOrderRepository _orderRepository;

        public OrderApiController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
            _response = new();
        }
        [HttpGet]
        public async Task<ActionResult<APIResponce>> GetOrders()
        {
            var orders = await _orderRepository.GetAllItemsAsync(includeProps: "CartItems,Status");
            _response.Result = orders.Select(p => new OrderViewModel(p));
            _response.StatusCode = System.Net.HttpStatusCode.OK;

            return Ok(_response);
        }

        [HttpGet("GetOrdersForUser/{userId:Guid}", Name = "GetOrdersForUser")]
        public async Task<ActionResult<APIResponce>> GetOrdersForUser(Guid userId)
        {
            var orders = await _orderRepository.GetAllItemsAsync(p => p.UserId == userId, includeProps: "CartItems,Status");
            _response.Result = orders.Select(p => new OrderViewModel(p));
            _response.StatusCode = System.Net.HttpStatusCode.OK;

            return Ok(_response);
        }

        [HttpGet("{id:Guid}", Name = "GetOrder")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<APIResponce>> GetOrder(Guid id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var order = await _orderRepository.GetItemAsync(p => p.Id == id, includeProps: "CartItems,Status");
            if (order == null)
            {
                return NotFound();
            }
            _response.Result = new OrderViewModel(order);
            _response.StatusCode = System.Net.HttpStatusCode.OK;

            return Ok(_response);
        }
        [HttpPost(Name = "CreateOrder")]
        public async Task<ActionResult<APIResponce>> CreateOrder([FromBody] OrderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model == null)
            {
                return BadRequest(model);
            }
            await _orderRepository.AddItemAsync(model);

            _response.Result = model;
            _response.StatusCode = System.Net.HttpStatusCode.Created;

            return CreatedAtRoute("GetOrder", new { id = model.Id }, _response);
        }

        [HttpDelete("{id:Guid}", Name = "DeleteOrder")]
        public async Task<ActionResult<APIResponce>> DeleteOrder(Guid id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var item = await _orderRepository.GetItemAsync(p => p.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            await _orderRepository.DeleteItemAsync(id);
            _response.StatusCode = HttpStatusCode.NoContent;
            _response.IsSuccess = true;
            return Ok(_response);
        }
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] OrderViewModel model)
        {
            if (model == null || id != model.Id)
            {
                return BadRequest();
            }
            await _orderRepository.ChangeItemAsync(model);
            _response.StatusCode = HttpStatusCode.NoContent;
            _response.IsSuccess = true;
            return Ok(_response);


        }
    }
}
