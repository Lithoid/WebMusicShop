using BL;
using Microsoft.AspNetCore.Mvc;
using Repositories;

using System.Net;

namespace StoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandApiController : ControllerBase
    {
        protected APIResponce _response;
        private IBrandRepository _brandRepository;

        public BrandApiController(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
            _response = new();
        }
        [HttpGet]
        public async Task<ActionResult<APIResponce>> GetBrands()
        {
            var categories = await _brandRepository.GetAllItemsAsync(includeProps:"Products");
            _response.Result = categories.Select(p => new BrandViewModel(p));
            _response.StatusCode = System.Net.HttpStatusCode.OK;

            return Ok(_response);
        }

        [HttpGet("{id:Guid}", Name = "GetBrand")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<APIResponce>> GetBrand(Guid id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var brand = await _brandRepository.GetItemAsync(p => p.Id == id,includeProps: "Products");
            if (brand == null)
            {
                return NotFound();
            }
            _response.Result = new BrandViewModel(brand);
            _response.StatusCode = System.Net.HttpStatusCode.OK;

            return Ok(_response);
        }
        [HttpPost(Name = "CreateBrand")]
        public async Task<ActionResult<APIResponce>> CreateBrand([FromBody] BrandViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model == null)
            {
                return BadRequest(model);
            }
            await _brandRepository.AddItemAsync(model);

            _response.Result = model;
            _response.StatusCode = System.Net.HttpStatusCode.Created;

            return CreatedAtRoute("GetBrand", new { id = model.Id }, _response);
        }

        [HttpDelete("{id:Guid}", Name = "DeleteBrand")]
        public async Task<ActionResult<APIResponce>> DeleteBrand(Guid id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var item = await _brandRepository.GetItemAsync(p => p.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            await _brandRepository.DeleteItemAsync(id);
            _response.StatusCode = HttpStatusCode.NoContent;
            _response.IsSuccess = true;
            return Ok(_response);
        }
        [HttpPut("{id:Guid}", Name = "UpdateBrand")]
        public IActionResult UpdateBrand(Guid id, [FromBody] BrandViewModel model)
        {
            if (model == null || id != model.Id)
            {
                return BadRequest();
            }
            _brandRepository.ChangeItemAsync(model);
            _response.StatusCode = HttpStatusCode.NoContent;
            _response.IsSuccess = true;
            return Ok(_response);


        }


    }
}
