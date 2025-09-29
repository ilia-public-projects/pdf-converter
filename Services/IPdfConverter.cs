using PdfConverterFunction.Models;

namespace PdfConverterFunction.Services
{
    public interface IPdfConverter
    {
        byte[] Convert(string html, string headerHtmlPath, PdfConfiguration pdfConfiguration = null);
    }
}