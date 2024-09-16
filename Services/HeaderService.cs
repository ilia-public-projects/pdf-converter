using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfConverterFunction.Services
{
    public class HeaderService : IHeaderService
    {
        public string WriteHtmlToFile(string headerHtml)
        {
            string path = "";
            if (!string.IsNullOrWhiteSpace(headerHtml))
            {
                Guid fileId = Guid.NewGuid();
                path = Path.Combine(Path.GetTempPath(), $"{fileId}.html");

                File.WriteAllText(path, headerHtml);
            }
            return path;
        }

        public void DeleteHtmlFile(string pathToHeader, ILogger logger)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(pathToHeader))
                {
                    File.Delete(pathToHeader);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to delete html header temp file, message: {ex.Message}");
            }
        }
    }
}
