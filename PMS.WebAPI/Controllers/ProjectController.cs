using Dapper;
using Microsoft.AspNetCore.Mvc;
using PMS.Infrastructure.DataAccess.Model;
using PMS.Infrastructure.DataAccess.Repo;
using System;
using System.Data;
using System.Threading.Tasks;


namespace PMS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IRepository<Projects> _repo;
        public ProjectController(IRepository<Projects> repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            ResponseModel<ProjectData> returnResponse = new ResponseModel<ProjectData>();

            try
            {
                var data = await _repo.GetAllAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                returnResponse.ReturnStatus = false;
                returnResponse.ReturnMessage.Add(ex.Message);
                return BadRequest(returnResponse);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            ResponseModel<ProjectData> returnResponse = new ResponseModel<ProjectData>();

            if (id == 0)
            {
                returnResponse.ReturnStatus = false;
                returnResponse.Errors.Add(1, ModelState);
                return BadRequest(returnResponse);
            }

            try
            {
                var data = await _repo.GetAsync(id, "p_id");
                return Ok(data);
            }
            catch (Exception ex)
            {
                returnResponse.ReturnStatus = false;
                returnResponse.ReturnMessage.Add(ex.Message);
                return BadRequest(returnResponse);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] ProjectData data)
        {
            ResponseModel<ProjectData> returnResponse = new ResponseModel<ProjectData>();
            if (!ModelState.IsValid)
            {
                returnResponse.ReturnStatus = false;
                returnResponse.Errors.Add(1, ModelState);
                return BadRequest(returnResponse);
            }

            var spParms = new DynamicParameters();
            spParms.Add("p_code", data.p_code, DbType.Int32);
            spParms.Add("p_mgrid", data.p_mgrid, DbType.Int32);
            spParms.Add("p_name", data.p_name, DbType.String);
            spParms.Add("p_startdate", DateTime.Parse(data.p_startdate).ToString("yyyy-MM-dd"), DbType.Date);
            spParms.Add("p_finishdate", DateTime.Parse(data.p_finishdate).ToString("yyyy-MM-dd"), DbType.Date);

            try
            {
                await _repo.InsertAsync("[dbo].[Add_Task_And_Project]", spParms, CommandType.StoredProcedure);
                return Ok(returnResponse);
            }
            catch (Exception ex)
            {
                returnResponse.ReturnStatus = false;
                returnResponse.ReturnMessage.Add(ex.Message);
                return BadRequest(returnResponse);
            }
        }



        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Projects data)
        {
            ResponseModel<Projects> returnResponse = new ResponseModel<Projects>();
            if (!ModelState.IsValid)
            {
                returnResponse.ReturnStatus = false;
                returnResponse.Errors.Add(1, ModelState);
                return BadRequest(returnResponse);
            }

            var spParms = new DynamicParameters();
            spParms.Add("Id", id, DbType.Int32);
            spParms.Add("name", data.p_name, DbType.String);

            try
            {
                await _repo.UpdateAsync($"UPDATE [dbo].[Projects] SET  p_name = @name  WHERE p_id = @Id", spParms, CommandType.Text);
                return Ok(returnResponse);
            }
            catch (Exception ex)
            {
                returnResponse.ReturnStatus = false;
                returnResponse.ReturnMessage.Add(ex.Message);
                return BadRequest(returnResponse);
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            ResponseModel<ProjectData> returnResponse = new ResponseModel<ProjectData>();
            if (id == 0)
            {
                returnResponse.ReturnStatus = false;
                returnResponse.Errors.Add(1, ModelState);
                return BadRequest(returnResponse);
            }

            try
            {
                await _repo.RemoveAsync(id, "p_id");
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
