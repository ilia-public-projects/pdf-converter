using DinkToPdf;
using PdfConverterFunction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfConverterFunction.Services
{
    public class PdfConverter : IPdfConverter
    {

        private SynchronizedConverter converter = new SynchronizedConverter(new PdfTools());

        public byte[] Convert(string html, string headerHtmlPath, PdfMarginConfiguration config = null)
        {

            HtmlToPdfDocument document = new HtmlToPdfDocument
            {
                GlobalSettings = new GlobalSettings
                {
                    PaperSize = PaperKind.A4,
                    Margins = new MarginSettings
                    {
                        Top = config?.Top ?? 30,
                        Bottom = config?.Bottom ?? 30,
                        Right = config?.Right ?? 10,
                        Left = config?.Left ?? 10
                    },

                },
                Objects =
                    {
                        new ObjectSettings
                        {

                            HtmlContent = html,
                            WebSettings = new WebSettings
                            {
                                EnableJavascript = true,
                                PrintMediaType= true,
                            },
                            HeaderSettings = string.IsNullOrWhiteSpace(headerHtmlPath) ? new HeaderSettings() : new HeaderSettings{ HtmUrl = headerHtmlPath },
                            FooterSettings = new FooterSettings{Left = "Page [page] / [topage]", FontSize=10, FontName="Helvetica Neue",Line = true, Spacing=10}
                        }
                    }
            };


            byte[] pdfBuf = converter.Convert(document);
            return pdfBuf;
        }
    }
}
