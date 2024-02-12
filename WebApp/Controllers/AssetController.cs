using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Context;
using Entities;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Services.IServices;
using Services;
using BL;
using System.Threading.Tasks;
using Domain;
using Newtonsoft.Json;
using System.Reflection.Metadata;
using NuGet.ContentModel;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetController : ControllerBase
    {
        
        private readonly IAssetService _assetService;
        private readonly IBlobService _blobService;


        public AssetController(IBlobService blobService, IAssetService assetService = null)
        {
            _blobService = blobService;
            _assetService = assetService;
        }

        [HttpPost]
        public async Task<List<Guid>> Add(IFormCollection formCollection)
        {
            List<Guid> result = new List<Guid>();
            if (formCollection.Files.Count > 0)
            {
                foreach (IFormFile filee in formCollection.Files)
                {
                    Guid id = Guid.NewGuid();
                    string ext = Path.GetExtension(filee.FileName);
                    AssetViewModel asset = new AssetViewModel
                    {
                        Id = id,
                        FileName = id.ToString() + ext,
                        OriginalFileName = filee.FileName,
                        MimeType = filee.ContentType,
                        FileExtention = ext
                    };
                    result.Add(id);

                    using (Stream stream = new FileStream("./Assets/" + asset.FileName,
                       FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read))
                    {
                        filee.CopyTo(stream);
                    }

                    //Blob
                    /* using (var stream = filee.OpenReadStream())
                     {
                         await _blobService.UploadFileAsync(stream, asset.FileName);
                     }*/


                    var response = await _assetService.CreateAsync<APIResponce>(asset, HttpContext.Session.GetString(SD.SessionToken));
                    if (response != null && response.IsSuccess)
                    {

                    }
                }

            }
            return result;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid? id)
        {
            if (!id.HasValue)
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);

            AssetViewModel asset = new();
            var response = await _assetService.GetAsync<APIResponce>(id.Value, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                asset = JsonConvert.DeserializeObject<AssetViewModel>(Convert.ToString(response.Result));
            }

            if (asset == null)
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);

            //Blob
            //BlobViewModel model = await _blobService.GetFileAsync(asset.FileName);

            //Local
            Stream s = new FileStream("./Assets/" + asset.FileName, FileMode.Open, FileAccess.Read, FileShare.Write);

            return new FileStreamResult(s, asset.MimeType);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (!id.HasValue) return new StatusCodeResult((int)HttpStatusCode.BadRequest);


            AssetViewModel asset = new();
            var response = await _assetService.GetAsync<APIResponce>(id.Value, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                asset = JsonConvert.DeserializeObject<AssetViewModel>(Convert.ToString(response.Result));
            }

            if (asset == null) return new StatusCodeResult((int)HttpStatusCode.BadRequest);


            //await _blobService.DeleteBlobAsync(asset.FileName);


            response = await _assetService.DeleteAsync<APIResponce>(asset.Id, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                asset = JsonConvert.DeserializeObject<AssetViewModel>(Convert.ToString(response.Result));
            }

            /*
            _context.Assets.Remove(asset);
            System.IO.File.Delete("./Assets/" + asset.FileName);
            _context.SaveChanges();
            */

            return Redirect("/Home/index");
        }

        [HttpPut]
        public async Task<IActionResult> Update(Guid? id, IFormCollection formCollection)
        {
            if (formCollection.Files.Count > 0)
            {
                foreach (IFormFile filee in formCollection.Files)
                {
                    AssetViewModel old = new();
                    var response = await _assetService.GetAsync<APIResponce>(id.Value, HttpContext.Session.GetString(SD.SessionToken));
                    if (response != null && response.IsSuccess)
                    {
                        old = JsonConvert.DeserializeObject<AssetViewModel>(Convert.ToString(response.Result));
                    }
                    string ext = Path.GetExtension(filee.FileName);

                    await _blobService.DeleteBlobAsync(old.FileName);
                    // System.IO.File.Delete("./Assets/" + old.Id);

                    old.FileName = old.Id.ToString() + ext;
                    old.FileExtention = ext;
                    old.MimeType = filee.ContentType;
                    old.OriginalFileName = old.FileName;

                    using (var stream = filee.OpenReadStream())
                    {
                        await _blobService.UploadFileAsync(stream, old.FileName);
                    }

                     response = await _assetService.UpdateAsync<APIResponce>(old, HttpContext.Session.GetString(SD.SessionToken));
                    if (response != null && response.IsSuccess)
                    {
                       
                    }
                }
            }

            return Redirect("/Home/Index");
        }
    }
}