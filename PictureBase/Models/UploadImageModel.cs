using Microsoft.AspNetCore.Http;

namespace PictureBase.Models
{
    public class UploadImageModel
    {
        public string Description { get; set; }
        public IFormFile File { get; set; }
    }
}
