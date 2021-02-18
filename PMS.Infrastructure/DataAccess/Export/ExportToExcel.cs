using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using PMS.Infrastructure.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace PMS.Infrastructure.DataAccess.Export
{
    public class ExportToExcel : ControllerBase, IExportToExcel
    {
        private readonly Func<DateTime, string> minDateFunc;

        public ExportToExcel()
        {
            minDateFunc = (d) =>
            {
                if (d == DateTime.MinValue || d == DateTime.MaxValue)
                {
                    return string.Empty;
                }
                return d.ToString("dd-MMM-yyyy");
            };
        }

        public Task<FileContentResult> Export(IEnumerable<MemoryReportTable> data)
        {
            var tcs = new TaskCompletionSource<FileContentResult>();
            Task.Run(() =>
            {
                var result = CalculateMode(data);
                tcs.SetResult(result);
            });

            return tcs.Task;
        }

        public FileContentResult CalculateMode(IEnumerable<MemoryReportTable> data)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Reports");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "p_id";
                worksheet.Cell(currentRow, 2).Value = "p_name";
                worksheet.Cell(currentRow, 3).Value = "p_code";
                worksheet.Cell(currentRow, 4).Value = "p_lvl";
                worksheet.Cell(currentRow, 5).Value = "p_startdate";
                worksheet.Cell(currentRow, 6).Value = "p_finishdate";
                worksheet.Cell(currentRow, 7).Value = "p_state";
                worksheet.Cell(currentRow, 8).Value = "t_id";
                worksheet.Cell(currentRow, 9).Value = "t_name";
                worksheet.Cell(currentRow, 10).Value = "t_description";
                worksheet.Cell(currentRow, 11).Value = "t_lvl";
                worksheet.Cell(currentRow, 12).Value = "t_startdate";
                worksheet.Cell(currentRow, 13).Value = "t_finishdate";
                worksheet.Cell(currentRow, 14).Value = "t_state";

                foreach (var d in data)
                {

                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = d.p_id;
                    worksheet.Cell(currentRow, 2).Value = d.p_name;
                    worksheet.Cell(currentRow, 3).Value = d.p_code;
                    worksheet.Cell(currentRow, 4).Value = d.p_lvl;
                    worksheet.Cell(currentRow, 5).Value = minDateFunc(d.p_startdate);
                    worksheet.Cell(currentRow, 6).Value = minDateFunc(d.p_finishdate);
                    worksheet.Cell(currentRow, 7).Value = d.p_state;
                    worksheet.Cell(currentRow, 8).Value = d.t_id;
                    worksheet.Cell(currentRow, 9).Value = d.t_name;
                    worksheet.Cell(currentRow, 10).Value = d.t_description;
                    worksheet.Cell(currentRow, 11).Value = d.t_lvl;
                    worksheet.Cell(currentRow, 12).Value = minDateFunc(d.t_startdate);
                    worksheet.Cell(currentRow, 13).Value = minDateFunc(d.t_finishdate);
                    worksheet.Cell(currentRow, 14).Value = d.t_state;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "users.xlsx");
                }
            }
        }
    }
}

