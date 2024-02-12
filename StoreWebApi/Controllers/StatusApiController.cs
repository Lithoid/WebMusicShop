using BL;
using Microsoft.AspNetCore.Mvc;
using Repositories;

using System.Net;

namespace StoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusApiController : ControllerBase
    {
        protected APIResponce _response;
        private IStatusRepository _statusRepository;

        public StatusApiController(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
            _response = new();
        }
        [HttpGet]
        public async Task<ActionResult<APIResponce>> GetStatuses()
        {
            var orders = await _statusRepository.GetAllItemsAsync();
            _response.Result = orders.Select(p => new StatusViewModel(p));
            _response.StatusCode = System.Net.HttpStatusCode.OK;

            return Ok(_response);
        }

        [HttpGet("{name}", Name = "GetStatus")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<APIResponce>> GetStatus(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest();
            }
            var order = await _statusRepository.GetItemAsync(p => p.Name == name);
            if (order == null)
            {
                return NotFound();
            }
            _response.Result = new StatusViewModel(order);
            _response.StatusCode = System.Net.HttpStatusCode.OK;

            return Ok(_response);
        }
        [HttpPost(Name = "CreateStatus")]
        public async Task<ActionResult<APIResponce>> CreateStatus([FromBody] StatusViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model == null)
            {
                return BadRequest(model);
            }
            await _statusRepository.AddItemAsync(model);

            _response.Result = model;
            _response.StatusCode = System.Net.HttpStatusCode.Created;

            return CreatedAtRoute("GetOrder", new { id = model.Id }, _response);
        }

        [HttpDelete("{id:Guid}", Name = "DeleteStatus")]
        public async Task<ActionResult<APIResponce>> DeleteStatus(Guid id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var item = await _statusRepository.GetItemAsync(p => p.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            await _statusRepository.DeleteItemAsync(id);
            _response.StatusCode = HttpStatusCode.NoContent;
            _response.IsSuccess = true;
            return Ok(_response);
        }
        [HttpPut("{id:Guid}", Name = "UpdateStatus")]
        public IActionResult UpdateOrder(Guid id, [FromBody] StatusViewModel model)
        {
            if (model == null || id != model.Id)
            {
                return BadRequest();
            }
            _statusRepository.ChangeItemAsync(model);
            _response.StatusCode = HttpStatusCode.NoContent;
            _response.IsSuccess = true;
            return Ok(_response);


        }
    }
}
