using BL;
using Microsoft.AspNetCore.Mvc;
using Repositories;

using System.Net;

namespace StoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryApiController : ControllerBase
    {

        protected APIResponce _response;
        private ICategoryRepository _categoryRepository;

        public CategoryApiController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _response = new();
        }
        [HttpGet]
        public async Task<ActionResult<APIResponce>> GetCategories()
        {
            var categories = await _categoryRepository.GetAllItemsAsync();
            _response.Result = categories.Select(p => new CategoryViewModel(p));
            _response.StatusCode = System.Net.HttpStatusCode.OK;

            return Ok(_response);
        }

        [HttpGet("{id:Guid}", Name = "GetCategory")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<APIResponce>> GetCategory(Guid id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var category = await _categoryRepository.GetItemAsync(p => p.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            _response.Result = new CategoryViewModel(category);
            _response.StatusCode = System.Net.HttpStatusCode.OK;

            return Ok(_response);
        }
        [HttpPost(Name = "CreateCategory")]
        public async Task<ActionResult<APIResponce>> CreateCategory([FromBody] CategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model == null)
            {
                return BadRequest(model);
            }
            await _categoryRepository.AddItemAsync(model);

            _response.Result = model;
            _response.StatusCode = System.Net.HttpStatusCode.Created;

            return CreatedAtRoute("GetCategory", new { id = model.Id }, _response);
        }

        [HttpDelete("{id:Guid}", Name = "DeleteCategory")]
        public async Task<ActionResult<APIResponce>> DeleteCategory(Guid id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var item = await _categoryRepository.GetItemAsync(p => p.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            await _categoryRepository.DeleteItemAsync(id);
            _response.StatusCode = HttpStatusCode.NoContent;
            _response.IsSuccess = true;
            return Ok(_response);
        }
        [HttpPut("{id:Guid}", Name = "UpdateCategory")]
        public IActionResult UpdateCategory(Guid id, [FromBody] CategoryViewModel model)
        {
            if (model == null || id != model.Id)
            {
                return BadRequest();
            }
            _categoryRepository.ChangeItemAsync(model);
            _response.StatusCode = HttpStatusCode.NoContent;
            _response.IsSuccess = true;
            return Ok(_response);


        }

    }
}
