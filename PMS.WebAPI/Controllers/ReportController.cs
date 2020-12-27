using ClosedXML.Excel;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using PMS.WebAPI.Model;
using PMS.WebAPI.Repo;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;


namespace PMS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IRepository<MemoryReportTable> _repo;
        public ReportController(IRepository<MemoryReportTable> repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            ResponseModel<ProjectReport> returnResponse = new ResponseModel<ProjectReport>();
            try
            {

                var data = await _repo.GetAsync("[dbo].[Report]", null, commandType: CommandType.StoredProcedure);

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
                        worksheet.Cell(currentRow, 5).Value = d.p_startdate;
                        worksheet.Cell(currentRow, 6).Value = d.p_finishdate;
                        worksheet.Cell(currentRow, 7).Value = d.p_state;
                        worksheet.Cell(currentRow, 8).Value = d.t_id; ;
                        worksheet.Cell(currentRow, 9).Value = d.t_name;
                        worksheet.Cell(currentRow, 10).Value = d.t_description;
                        worksheet.Cell(currentRow, 11).Value = d.t_lvl;
                        worksheet.Cell(currentRow, 12).Value = d.t_startdate;
                        worksheet.Cell(currentRow, 13).Value = d.t_finishdate;
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
            catch (Exception ex)
            {
                returnResponse.ReturnStatus = false;
                returnResponse.ReturnMessage.Add(ex.Message);
                return BadRequest(returnResponse);
            }
        }

    }
}
