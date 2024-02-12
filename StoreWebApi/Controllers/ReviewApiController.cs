using BL;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using System.Net;

namespace StoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewApiController : ControllerBase
    {

        protected APIResponce _response;
        private IReviewRepository _reviewRepository;

        public ReviewApiController(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
            _response = new();
        }
        [HttpGet]
        public async Task<ActionResult<APIResponce>> GetReviews()
        {
            var reviews = await _reviewRepository.GetAllItemsAsync();
            _response.Result = reviews.Select(p => new ReviewViewModel(p));
            _response.StatusCode = System.Net.HttpStatusCode.OK;

            return Ok(_response);
        }

        [HttpGet("{id:Guid}", Name = "GetReview")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<APIResponce>> GetReview(Guid id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var review = await _reviewRepository.GetItemAsync(p => p.Id == id);
            if (review == null)
            {
                return NotFound();
            }
            _response.Result = new ReviewViewModel(review);
            _response.StatusCode = System.Net.HttpStatusCode.OK;

            return Ok(_response);
        }

        [HttpGet("GetAllForProduct/{productId:Guid}", Name = "GetAllForProduct")]
        public async Task<ActionResult<APIResponce>> GetAllForProduct(Guid productId)
        {
            var reviews = await _reviewRepository.GetAllItemsAsync(p => p.ProductId == productId, includeProps: "Product");
            _response.Result = reviews.Select(p => new ReviewViewModel(p));
            _response.StatusCode = System.Net.HttpStatusCode.OK;

            return Ok(_response);
        }
        [HttpGet("GetReviewsForUser/{userId:Guid}", Name = "GetReviewsForUser")]
        public async Task<ActionResult<APIResponce>> GetReviewsForUser(Guid userId)
        {
            var reviews = await _reviewRepository.GetAllItemsAsync(p => p.UserId == userId, includeProps: "Product");
            _response.Result = reviews.Select(p => new ReviewViewModel(p));
            _response.StatusCode = System.Net.HttpStatusCode.OK;

            return Ok(_response);
        }









        [HttpPost(Name = "CreateReview")]
        public async Task<ActionResult<APIResponce>> CreateReview([FromBody] ReviewViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model == null)
            {
                return BadRequest(model);
            }
            await _reviewRepository.AddItemAsync(model);

            _response.Result = model;
            _response.StatusCode = System.Net.HttpStatusCode.Created;

            return CreatedAtRoute("GetCategory", new { id = model.Id }, _response);
        }

        [HttpDelete("{id:Guid}", Name = "DeleteReview")]
        public async Task<ActionResult<APIResponce>> DeleteReview(Guid id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var item = await _reviewRepository.GetItemAsync(p => p.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            await _reviewRepository.DeleteItemAsync(id);
            _response.StatusCode = HttpStatusCode.NoContent;
            _response.IsSuccess = true;
            return Ok(_response);
        }
        [HttpPut("{id:Guid}", Name = "UpdateReview")]
        public IActionResult UpdateReview(Guid id, [FromBody] ReviewViewModel model)
        {
            if (model == null || id != model.Id)
            {
                return BadRequest();
            }
            _reviewRepository.ChangeItemAsync(model);
            _response.StatusCode = HttpStatusCode.NoContent;
            _response.IsSuccess = true;
            return Ok(_response);


        }
    }
}
