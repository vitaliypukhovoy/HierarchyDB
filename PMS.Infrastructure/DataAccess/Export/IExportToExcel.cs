using Microsoft.AspNetCore.Mvc;
using PMS.Infrastructure.DataAccess.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMS.Infrastructure.DataAccess.Export
{
    public interface IExportToExcel
    {
        Task<FileContentResult> Export(IEnumerable<MemoryReportTable> data);
        FileContentResult CalculateMode(IEnumerable<MemoryReportTable> data);
    }
}
