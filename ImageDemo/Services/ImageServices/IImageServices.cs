using ImageDemo.Data;
using ImageDemo.Models;
using System.Web;

//using static System.Net.Mime.MediaTypeNames;


namespace ImageDemo.Services.ImageServices
{
    public interface IImageServices
    {
        public void Upload(IFormFile file);
        public List<Image> GetImage();
    }
}
