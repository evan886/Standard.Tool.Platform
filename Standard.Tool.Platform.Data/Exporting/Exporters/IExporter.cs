using Standard.Tool.Platform.Data.Import;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Standard.Tool.Platform.Data.Exporting.Exporters
{
    public interface IExporter<T>
    {
        Task<ExportResult> ExportData<TResult>(Expression<Func<T, TResult>> selector, CancellationToken ct);
    }
}
