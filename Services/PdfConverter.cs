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

        public byte[] Convert(string html, string headerHtmlPath, PdfConfiguration config = null)
        {
            var marginConfig = config?.Margins ?? new PdfMarginConfiguration();
            var orientation = config?.Orientation == PdfOrientation.Landscape ? Orientation.Landscape : Orientation.Portrait;

            HtmlToPdfDocument document = new HtmlToPdfDocument
            {
                GlobalSettings = new GlobalSettings
                {
                    PaperSize = PaperKind.A4,
                    Margins = new MarginSettings
                    {
                        Top = marginConfig?.Top ?? 30,
                        Bottom = marginConfig?.Bottom ?? 30,
                        Right = marginConfig?.Right ?? 10,
                        Left = marginConfig?.Left ?? 10
                    },
                    Orientation = orientation

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
                            FooterSettings = new FooterSettings{
                                Left = "Page [page] / [topage]",
                                FontSize=10,
                                FontName="OPEN SANS",
                                Line = true,
                                Spacing=10,
                                Right = "[date] [time]",
                            }
                        }
                    }
            };


            byte[] pdfBuf = converter.Convert(document);
            return pdfBuf;
        }
    }
}
