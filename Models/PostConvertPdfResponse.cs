using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfConverterFunction.Models
{
    /// <summary>
    /// Response object containing converter html to pdf
    /// </summary>
    public class PostConvertPdfResponse
    {
        /// <summary>
        /// Pdf document as a byte array
        /// </summary>
        public byte[] Pdf { get; set; }
    }
}
