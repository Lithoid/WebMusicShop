using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using BL;

namespace Services.IServices
{

    public interface IBlobService
    {
        public Task<BlobViewModel> GetFileAsync(string name);

        public Task<List<string>> ListBlobsAsync();
        public Task<string> UploadFileAsync(Stream fileStream, string fileName);
        public Task DeleteBlobAsync(string blobName);


    }
}
