using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace SuperShop_Mariana.Helpers
{
    public interface IImageHelper
    {
        Task<string> UploadImageAsync(IFormFile imageFile, string folder); //Assinatura do método
        
    }
}
