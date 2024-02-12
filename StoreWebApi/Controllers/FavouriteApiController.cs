using BL;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Repositories.generic;
using System.Net;

namespace StoreWebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class FavouriteApiController : Controller
    {
        protected APIResponce _response;
        private IFavouriteRepository _favouriteRepository;

        public FavouriteApiController(IFavouriteRepository favouriteRepository)
        {
            _favouriteRepository = favouriteRepository;
            _response = new();
        }
        [HttpGet]
        public async Task<ActionResult<APIResponce>> GetFavourites()
        {
            var products = await _favouriteRepository.GetAllItemsAsync();
            _response.Result = products.Select(p => new FavouriteViewModel(p));
            _response.StatusCode = System.Net.HttpStatusCode.OK;

            return Ok(_response);
        }
        [HttpPost]
        public async Task<ActionResult<APIResponce>> CreateFavourite([FromBody] FavouriteViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model == null)
            {
                return BadRequest(model);
            }
            await _favouriteRepository.AddItemAsync(model);

            _response.Result = model;
            _response.StatusCode = System.Net.HttpStatusCode.Created;

            return CreatedAtRoute("GetFavourite", new { id = model.Id }, _response);
        }

        [HttpDelete("{prodId:Guid}/{userId:Guid}", Name = "DeleteFavourite")]
        public async Task<ActionResult<APIResponce>> DeleteFavourite(Guid prodId,Guid userId)
        {
            if (prodId == Guid.Empty || userId == Guid.Empty)
            {
                return BadRequest();
            }
            var item = await _favouriteRepository.GetItemAsync(f =>f.UserId==userId&&f.ProductId==prodId );
            if (item == null)
            {
                return NotFound();
            }
            await _favouriteRepository.DeleteItemAsync(item.Id);
            _response.StatusCode = HttpStatusCode.NoContent;
            _response.IsSuccess = true;
            return Ok(_response);
        }
      

    }
}
