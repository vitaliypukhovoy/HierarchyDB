using Microsoft.AspNetCore.Mvc;
using PMS.Infrastructure.DataAccess.Export;
using PMS.Infrastructure.DataAccess.Model;
using PMS.Infrastructure.DataAccess.Repo;
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

        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            ResponseModel<ProjectReport> returnResponse = new ResponseModel<ProjectReport>();

            try
            {
                var data = await _repo.GetAsync("[dbo].[Report]", null, commandType: CommandType.StoredProcedure);
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
