using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfConverterFunction.Models
{
    /// <summary>
    /// Request object containing source html and communication secret
    /// </summary>
    public class PostConvertPdfRequest
    {
        /// <summary>
        /// Html as a string, which wil be converted to pdf
        /// </summary>
        public string Html { get; set; }
        /// <summary>
        /// Name of the file
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// Communication secret
        /// </summary>
        public string Secret { get; set; }
        /// <summary>
        /// If set will render html in the header of the pdf
        /// </summary>
        public string HeaderHtml { get; set; }
        public PdfMarginConfiguration PdfMarginConfiguration { get; set; }
    }
}
