using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Syncfusion.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Syncfusion.DocIO;
using Syncfusion.Pdf;
using System.IO;
using Syncfusion.DocIO.DLS;
using Syncfusion.DocIORenderer;

namespace Syncfusion.Controllers
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

        public IActionResult Pdf()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "WordtoPDF.docx");
            FileStream fileStreamPath = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

            WordDocument document = new WordDocument(fileStreamPath, FormatType.Automatic);
            fileStreamPath.Dispose();
            fileStreamPath = null;
            // Creates a new instance of DocIORenderer class.
            DocIORenderer.DocIORenderer render = new DocIORenderer.DocIORenderer();

            PdfDocument pdf = render.ConvertToPDF(document);
            MemoryStream memoryStream = new MemoryStream();
            // Save the PDF document.
            pdf.Save(memoryStream);
            render.Dispose();
            pdf.Close();
            document.Close();
            memoryStream.Position = 0;

            /*
            WordDocument wordDocument = new WordDocument("Sample.docx", FormatType.Docx);
            //Create an instance of DocToPDFConverter
            DocToPDFConverter converter = new DocToPDFConverter();
            //Convert Word document into PDF document
            PdfDocument pdfDocument = converter.ConvertToPDF(wordDocument);
            //Save the PDF file
            pdfDocument.Save("WordtoPDF.pdf");
            //Close the instance of document objects
            pdfDocument.Close(true);
            wordDocument.Close();
            */



            return File(memoryStream, "application/pdf", "WordtoPDF.pdf");
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
