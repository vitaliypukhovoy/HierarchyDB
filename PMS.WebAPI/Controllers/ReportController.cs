using Dapper;
using Microsoft.AspNetCore.Mvc;
using PMS.Infrastructure.DataAccess.Context;
using PMS.Infrastructure.DataAccess.Export;
using PMS.Infrastructure.DataAccess.Model;
using PMS.Infrastructure.DataAccess.Repo;
using PMS.Infrastructure.IoC;
using System;
using System.Data;
using System.Threading.Tasks;


namespace PMS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IRepository<MemoryReportTable> _repo;
        private readonly IExportToExcel _export;
        public ReportController(IRepository<MemoryReportTable> repo, IExportToExcel export)
        {
            _repo = repo;
            _export = export;
        }

        // query string report?startDate= 1-12-2000&finishDate= 12-12-2012
        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> GetAsync([FromQuery] ProjectReport projectReport)
        {
            ResponseModel<ProjectReport> returnResponse = new ResponseModel<ProjectReport>();

            if (!ModelState.IsValid)
            {
                returnResponse.ReturnStatus = false;
                returnResponse.Errors.Add(1, ModelState);
                return BadRequest(returnResponse);
            }


            var spParms = new DynamicParameters();
            spParms.Add("p_startdate", projectReport.startDate, DbType.Date);
            spParms.Add("p_finishdate", projectReport.finishDate, DbType.Date);

            try
            {
                var data = await _repo.GetAsync("[dbo].[Report]", spParms, commandType: CommandType.StoredProcedure);
                return await _export.Export(data);
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
