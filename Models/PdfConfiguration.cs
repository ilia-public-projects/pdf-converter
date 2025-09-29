using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfConverterFunction.Models
{
    public class PdfConfiguration
    {
        public PdfMarginConfiguration Margins { get; set; } = new PdfMarginConfiguration();
        public PdfOrientation Orientation { get; set; } = PdfOrientation.Portrait;
    }
}
