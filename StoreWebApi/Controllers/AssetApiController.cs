using BL;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using System.Net;

namespace StoreWebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AssetApiController : ControllerBase
    {
        protected APIResponce _response;
        private IAssetRepository _assetRepository;

        public AssetApiController(IAssetRepository assetRepository)
        {
            _assetRepository = assetRepository;
            _response = new();
        }
        [HttpGet]
        public async Task<ActionResult<APIResponce>> GetAssets()
        {
            var assets = await _assetRepository.GetAllItemsAsync(includeProps: "Products");
            _response.Result = assets.Select(p => new AssetViewModel(p));
            _response.StatusCode = System.Net.HttpStatusCode.OK;

            return Ok(_response);
        }
        [HttpGet("{id:Guid}", Name = "GetAsset")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<APIResponce>> GetAsset(Guid id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var asset = await _assetRepository.GetItemAsync(p => p.Id == id,includeProps:"Products");
            if (asset == null)
            {
                return NotFound();
            }
            _response.Result = new AssetViewModel(asset);
            _response.StatusCode = System.Net.HttpStatusCode.OK;

            return Ok(_response);
        }

        [HttpPost(Name = "CreateAsset")]
        public async Task<ActionResult<APIResponce>> CreateAsset([FromBody] AssetViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model == null)
            {
                return BadRequest(model);
            }
            await _assetRepository.AddItemAsync(model);

            _response.Result = model;
            _response.StatusCode = System.Net.HttpStatusCode.Created;

            return CreatedAtRoute("GetAsset", new { id = model.Id }, _response);
        }
        [HttpDelete("{id:Guid}", Name = "DeleteAsset")]
        public async Task<ActionResult<APIResponce>> DeleteAsset(Guid id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var item = await _assetRepository.GetItemAsync(p => p.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            await _assetRepository.DeleteItemAsync(id);
            _response.StatusCode = HttpStatusCode.NoContent;
            _response.IsSuccess = true;
            return Ok(_response);
        }
        [HttpPut("{id:Guid}", Name = "UpdateAsset")]
        public IActionResult UpdateAsset(Guid id, [FromBody] AssetViewModel model)
        {
            if (model == null || id != model.Id)
            {
                return BadRequest();
            }
            _assetRepository.ChangeItemAsync(model);
            _response.StatusCode = HttpStatusCode.NoContent;
            _response.IsSuccess = true;
            return Ok(_response);


        }
    }
}
