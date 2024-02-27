using ImageDemo.Models;
using ImageDemo.Services.ImageServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
//using static System.Net.Mime.MediaTypeNames;

namespace ImageDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IImageServices _imageServices;

        public HomeController(ILogger<HomeController> logger, IImageServices imageServices)
        {
            _logger = logger;
            _imageServices = imageServices;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(IFormFile file)
        {
            _imageServices.Upload(file);
            return RedirectToAction("GetImage", "Home");
        }

        [HttpGet]
        public IActionResult GetImage()
        {
            var images = _imageServices.GetImage();
            
            return View(images);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
