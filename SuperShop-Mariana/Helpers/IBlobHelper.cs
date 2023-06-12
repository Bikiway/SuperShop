using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace SuperShop_Mariana.Helpers
{
    public interface IBlobHelper
    {
        Task<Guid> UploadBlobAsync(IFormFile file, string containerName); //recebe um ficheiro formulario

        Task<Guid> UploadBlobAsync(byte[] file, string containerName); //Array de bytes. Serve para passar uma rede de imagens. ex: tlmv para azure.

        Task<Guid> UploadBlobAsync(string image, string containerName); //string de imagem
    }
}
