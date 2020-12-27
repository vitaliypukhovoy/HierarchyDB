using Dapper;
using Microsoft.AspNetCore.Mvc;
using PMS.WebAPI.Model;
using PMS.WebAPI.Repo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;


namespace PMS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IRepository<Tasks> _repo;
        public ReportController(IRepository<Tasks> repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            ResponseModel<ProjectData> returnResponse = new ResponseModel<ProjectData>();            
            try
            {
                await _repo.InsertAsync("[dbo].[Rreport]", null, commandType: CommandType.StoredProcedure);
                return Ok(returnResponse);
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
