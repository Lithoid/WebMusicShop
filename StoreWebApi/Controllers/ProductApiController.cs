using BL;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories;

using System.Data;
using System.Net;

namespace StoreWebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductApiController : ControllerBase
    {
        protected APIResponce _response;
        private IProductRepository _productRepository;

        public ProductApiController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            _response = new();
        }
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<APIResponce>> GetProducts()
        {
            var products = await _productRepository.GetAllItemsAsync(includeProps: "Brand,Category,Assets,Favourites,Reviews");
            _response.Result = products.Select(p=>new ProductViewModel(p));
            _response.StatusCode = System.Net.HttpStatusCode.OK;

            return Ok(_response);
        }
        [HttpGet("GetProductsByCategory/{categoryId:Guid}", Name = "GetProductsByCategory")]
        public async Task<ActionResult<APIResponce>> GetProductsByCategory(Guid categoryId)
        {
            var products = await _productRepository.GetAllItemsAsync(p=>p.CategoryId== categoryId, includeProps: "Brand,Category,Assets,Favourites,Reviews");
            _response.Result = products.Select(p => new ProductViewModel(p));
            _response.StatusCode = System.Net.HttpStatusCode.OK;

            return Ok(_response);
        }

        [HttpGet("{id:Guid}", Name = "GetProduct")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<APIResponce>> GetProduct(Guid id)
        {
            if (id==null)
            {
                return BadRequest();
            }
            var product = await _productRepository.GetItemAsync(p=>p.Id==id,includeProps: "Brand,Category,Assets,Favourites,Reviews");
            if (product == null)
            {
                return NotFound();
            }
            _response.Result = new ProductViewModel(product);
            _response.StatusCode = System.Net.HttpStatusCode.OK;

            return Ok(_response);
        }
        [HttpPost( Name = "CreateProduct")]
        public async Task<ActionResult<APIResponce>> CreateProduct([FromBody] ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model == null)
            {
                return BadRequest(model);
            }

            await _productRepository.AddItemAsync(model);

            _response.Result = model;
            _response.StatusCode = System.Net.HttpStatusCode.Created;

            return CreatedAtRoute("GetProduct", new { id = model.Id }, _response);
        }

        [HttpDelete("{id:Guid}", Name = "DeleteProduct")]
        public async Task<ActionResult<APIResponce>> DeleteProduct(Guid id)
        {
            if (id==null)
            {
                return BadRequest();
            }
            var villa = await _productRepository.GetItemAsync(p => p.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            await _productRepository.DeleteItemAsync(id);
            _response.StatusCode = HttpStatusCode.NoContent;
            _response.IsSuccess = true;
            return Ok(_response);
        }
        [HttpPut("{id:Guid}", Name = "UpdateProduct")]
        public async Task<ActionResult<APIResponce>> UpdateProduct(Guid id, [FromBody] ProductViewModel model)
        {
            if (model == null || id != model.Id)
            {
                return BadRequest();
            }
            await _productRepository.ChangeItemAsync(model);
            _response.StatusCode = HttpStatusCode.NoContent;
            _response.IsSuccess = true;
            return Ok(_response);


        }
       
        [HttpGet("GetFavouriteForUser/{userId:Guid}", Name = "GetFavouriteForUser")]
        public async Task<ActionResult<APIResponce>> GetFavouriteForUser(Guid userId)
        {
            var products = await _productRepository.GetAllItemsAsync(p => p.Favourites.Any(f=>f.UserId== userId), includeProps: "Brand,Category,Assets,Favourites,Reviews");
            _response.Result = products.Select(p => new ProductViewModel(p));
            _response.StatusCode = System.Net.HttpStatusCode.OK;

            return Ok(_response);
        }

    }
}
