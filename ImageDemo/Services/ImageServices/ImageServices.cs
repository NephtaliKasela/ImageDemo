using ImageDemo.Data;
using ImageDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Web;
//using static System.Net.Mime.MediaTypeNames;

namespace ImageDemo.Services.ImageServices
{
    public class ImageServices: IImageServices
    {
        private readonly DataContext _context;

        public ImageServices(DataContext context)
        {
            _context = context;
        }

        public void Upload(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    file.CopyTo(memoryStream);
                    var imageData = memoryStream.ToArray();

                    // Save the imageData to the database using your data access logic
                    // For example, using Entity Framework Core:
                    Image image = new Image
                    {
                        // Set other properties of the model
                        ImageData = imageData,
                        FileName = file.FileName,
                        ContentType = GetImageContentType(file.FileName)
                        
                    };

                    // Save the model to the database
                    // dbContext is your instance of the database context
                    _context.Images.Add(image);
                    _context.SaveChanges();
                }
            }
        }

        public List<Image> GetImage()
        {
            var images = _context.Images.ToList();

            return images;

        }


        private string GetImageContentType(string fileName)
        {
            return Path.GetExtension(fileName)?.ToLowerInvariant();

            //return string extension = Path.GetExtension(fileName)?.ToLowerInvariant();

            //switch (extension)
            //{
            //    case ".jpg":
            //    case ".jpeg":
            //        return "image/jpeg";
            //    case ".png":
            //        return "image/png";
            //    case ".gif":
            //        return "image/gif";
            //    // Add more cases for other image formats if needed
            //    default:
            //        return "application/octet-stream"; // Default to binary data if the format is unknown
            //}
        }
    }
}
