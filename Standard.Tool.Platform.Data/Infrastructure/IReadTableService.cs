using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standard.Tool.Platform.Data.Infrastructure
{
    public interface IReadTableService
    {
        DataTable ReadCsv(Stream stream);
        DataSet ReadExcel();
        string NormalizeString(string columnName);
    }
}
