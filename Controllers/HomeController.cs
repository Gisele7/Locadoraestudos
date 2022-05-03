using Filme_Locadora.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Filme_Locadora.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(UploadModel upload)
        {
            var FileDic = "Files";
            string FilePath = Path.Combine("c:\\teste\\", FileDic);

            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }
            var fileName = upload.File.Name;
            var filePath = Path.Combine(FilePath, upload.File.FileName);


            using (FileStream fs = System.IO.File.Create(filePath))
            {
                upload.File.CopyTo(fs);

            }

            return View();

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