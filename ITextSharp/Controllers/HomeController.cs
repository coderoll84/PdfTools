using ITextSharp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ITextSharp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Pdf()
        {
            var x = new GeneratePdf();

            var entity = new DemoModel { Name = "Oscar", Date = DateTime.Now };
            var file = x.NewPdf(entity);

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", file);
            var fileBytes = System.IO.File.ReadAllBytes(path);
            return File(fileBytes, "application/pdf");
        }

        public IActionResult Index()
        {
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
