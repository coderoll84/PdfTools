using iText.Forms;
using iText.Forms.Fields;
using iText.Kernel.Pdf;
using ITextSharp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ITextSharp
{
    public class GeneratePdf
    {
        public GeneratePdf()
        {

        }

        public string NewPdf(DemoModel model) {
            var newPdf = $"{Guid.NewGuid()}.pdf";
            var newPdfRute = $"wwwroot\\{newPdf}";
            var templateFile = @"wwwroot\template.pdf";

            using (var template = new FileStream(templateFile,FileMode.Open)) 
            {
                using (var newFile = new FileStream(newPdfRute, FileMode.Create))
                {
                    PdfDocument pdf = new PdfDocument(new PdfReader(template), new PdfWriter(newFile));
                    PdfAcroForm form = PdfAcroForm.GetAcroForm(pdf, true);
                    IDictionary<string, PdfFormField> fields = form.GetFormFields();
                    
                    PdfFormField name = fields["name"];
                    PdfFormField date = fields["date"];

                    name.SetValue(model.Name);
                    date.SetValue(model.Date.ToString("dd/MM/yyyy"));

                    form.FlattenFields();
                    pdf.Close();

                    return newPdf;
                }
            }
        }
    }
}
