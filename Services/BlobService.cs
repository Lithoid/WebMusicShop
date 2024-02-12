using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using BL;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BlobService : IBlobService
    {

        private readonly BlobServiceClient _blobServiceClient;
        private BlobContainerClient client;
        public static readonly List<string> ImageExtensions = new List<string> { ".JPG", ".JPEG", ".PNG" };

        public BlobService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
            client = _blobServiceClient.GetBlobContainerClient("assetscontainer");
        }

        public async Task<string> UploadFileAsync(Stream fileStream, string fileName)
        {
            var blobClient = client.GetBlobClient(fileName);
            var response = await blobClient.UploadAsync(fileStream);

            

            return blobClient.Uri.AbsoluteUri;

        }
        public async Task<BlobViewModel> GetFileAsync(string fileName)
        {
            try
            {
                var blobClient = client.GetBlobClient(fileName);
                if (await blobClient.ExistsAsync())
                {
                    BlobDownloadResult content = await blobClient.DownloadContentAsync();
                    var downloadedData = content.Content.ToStream();

                    if (ImageExtensions.Contains(Path.GetExtension(fileName.ToUpperInvariant())))
                    {
                        var extension = Path.GetExtension(fileName);
                        return new BlobViewModel { Content = downloadedData, ContentType = "image/" + extension.Remove(0, 1) };
                    }
                    else
                    {
                        return new BlobViewModel { Content = downloadedData, ContentType = content.Details.ContentType };
                    }

                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                throw;
            }

        }


        public async Task DeleteBlobAsync(string path)
        {
            var fileName = new Uri(path).Segments.LastOrDefault();
            var blobClient = client.GetBlobClient(fileName);
            await blobClient.DeleteIfExistsAsync();
        }



        public async Task<List<string>> ListBlobsAsync()
        {
            List<string> lst = new List<string>();

            await foreach (var blobItem in client.GetBlobsAsync())
            {
                lst.Add(blobItem.Name);
            }

            return lst;
        }


    }
}
