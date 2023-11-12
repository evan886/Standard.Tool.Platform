using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standard.Tool.Platform.Data.Exporting
{
    public class ExportResult
    {
        public ExportFormat ExportFormat { get; set; }

        public string? FilePath { get; set; }

        public byte[]? Content { get; set; }

        public string ContentType
        {
            get
            {
                return ExportFormat switch
                {
                    ExportFormat.SingleCSVFile => "text/csv",
                    _ => string.Empty
                };
            }
        }
    }

}
