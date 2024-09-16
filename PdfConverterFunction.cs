using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PdfConverterFunction.Models;
using PdfConverterFunction.Services;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PdfConverterFunction
{
    public class PdfConverterFunction
    {
        private readonly ILogger<PdfConverterFunction> logger;
        private readonly IHeaderService headerService;
        private readonly IConfiguration configuration;
        private readonly IPdfConverter pdfConverter;

        public PdfConverterFunction(
            ILogger<PdfConverterFunction> logger,
            IHeaderService headerService, 
            IConfiguration configuration,
            IPdfConverter pdfConverter)
        {
            this.logger = logger;
            this.headerService = headerService;
            this.configuration = configuration;
            this.pdfConverter = pdfConverter;
        }

        [FunctionName("pdfconverter")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "convert")] HttpRequest req)
        {
            string communicationSecret = configuration.GetSection("AuthSecret").Value;
            logger.LogInformation("C# HTTP trigger function processed a request.");

            using (StreamReader streamReader = new StreamReader(req.Body))
            {
                var requestBody = await streamReader.ReadToEndAsync();
                var request = JsonConvert.DeserializeObject<PostConvertPdfRequest>(requestBody);

                string headerPath = "";
                try
                {

                    if (request?.Secret != communicationSecret && !string.IsNullOrEmpty(communicationSecret))
                    {
                        throw new InvalidOperationException("Invalid secret");
                    }

                    if (string.IsNullOrWhiteSpace(request?.Html))
                    {
                        throw new ArgumentNullException("Html cannot be blank");
                    }

                    headerPath = headerService.WriteHtmlToFile(request.HeaderHtml);                    

                    byte[] result = pdfConverter.Convert(request.Html, headerPath, request.PdfMarginConfiguration);
                    string fileName = string.IsNullOrWhiteSpace(request.FileName) ? "converted-to.pdf" : request.FileName;

                    return new FileContentResult(result, "application/pdf") { FileDownloadName = fileName };

                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"An error occured whiel converting html to pdf, message: {ex.Message}");
                    var errorResult = new
                    {
                        message = "Process stopped due to a critical error",
                        exception = ex
                    };
                    return new BadRequestObjectResult(errorResult);
                }
                finally
                {
                    headerService.DeleteHtmlFile(headerPath, logger);
                }
            }
        }
    }
}
