using Microsoft.Extensions.Logging;

namespace PdfConverterFunction.Services
{
    public interface IHeaderService
    {
        void DeleteHtmlFile(string pathToHeader, ILogger logger);
        string WriteHtmlToFile(string headerHtml);
    }
}