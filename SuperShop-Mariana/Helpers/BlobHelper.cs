using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SuperShop_Mariana.Helpers
{
    public class BlobHelper : IBlobHelper
    {
        //Criar um client

        private readonly CloudBlobClient _blobClient;

        public BlobHelper(IConfiguration configuration)
        {
            string keys = configuration["Blob:ConnectionString"]; //Vai ao ficheiro de configuração e injetas aqui.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(keys); //Ligação ao storage
            _blobClient = storageAccount.CreateCloudBlobClient();
        }

        public async Task<Guid> UploadBlobAsync(IFormFile file, string containerName)
        {
           Stream stream = file.OpenReadStream();
            return await UploadBlobAsync(stream, containerName);
        }

        public async Task<Guid> UploadBlobAsync(byte[] file, string containerName)
        {
           MemoryStream stream = new MemoryStream(file);
            return await UploadBlobAsync(stream, containerName);
        }

        public async Task<Guid> UploadBlobAsync(Stream stream, string containerName)
        {
            Guid name = Guid.NewGuid();
            CloudBlobContainer container = _blobClient.GetContainerReference(containerName);
            CloudBlockBlob cloudBlock = container.GetBlockBlobReference($"{name}");
            await cloudBlock.UploadFromStreamAsync(stream);
            return name;
        }

        public async Task<Guid> UploadBlobAsync(string image, string containerName)
        {
            Stream stream = File.OpenRead(image);
            return await UploadBlobAsync(stream, containerName);
        }
    }
}
