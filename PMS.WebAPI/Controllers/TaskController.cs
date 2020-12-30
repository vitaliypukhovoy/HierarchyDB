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
    public class TaskController : ControllerBase
    {
        private readonly IRepository<Tasks> _repo;
        public TaskController(IRepository<Tasks> repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            ResponseModel<TaskData> returnResponse = new ResponseModel<TaskData>();

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
            ResponseModel<TaskData> returnResponse = new ResponseModel<TaskData>();

            if (id == 0)
            {
                returnResponse.ReturnStatus = false;
                returnResponse.Errors.Add(1, ModelState);
                return BadRequest(returnResponse);
            }

            try
            {
                var data = await _repo.GetAsync(id, "t_id");
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
        public async Task<ActionResult> Create([FromBody] TaskData data)
        {
            ResponseModel<TaskData> returnResponse = new ResponseModel<TaskData>();
            if (!ModelState.IsValid)
            {
                returnResponse.ReturnStatus = false;
                returnResponse.Errors.Add(1, ModelState);
                return BadRequest(returnResponse);
            }

            var spParms = new DynamicParameters();
            spParms.Add("p_id", data.p_id, DbType.Int32);
            spParms.Add("t_name", data.t_name, DbType.String);
            spParms.Add("t_mgrid", data.t_mgrid, DbType.Int32);
            spParms.Add("t_startdate", DateTime.Parse(data.t_startdate), DbType.Date);
            spParms.Add("t_finishdate", DateTime.Parse(data.t_finishdate), DbType.Date);
            spParms.Add("t_state", data.t_state, DbType.Int32);

            try
            {
                await _repo.InsertAsync("[dbo].[Add_Task]", spParms, commandType: CommandType.StoredProcedure);
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
        public async Task<ActionResult> Put(int id, [FromBody] Tasks data)
        {
            ResponseModel<Tasks> returnResponse = new ResponseModel<Tasks>();
            if (!ModelState.IsValid)
            {
                returnResponse.ReturnStatus = false;
                returnResponse.Errors.Add(1, ModelState);
                return BadRequest(returnResponse);
            }

            var spParms = new DynamicParameters();
            spParms.Add("Id", id, DbType.Int32);
            spParms.Add("name", data.t_name, DbType.String);

            try
            {
                await _repo.UpdateAsync($"UPDATE [dbo].[Tasks] SET  t_name = @name  WHERE t_id = @Id", spParms, CommandType.Text);
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
            ResponseModel<TaskData> returnResponse = new ResponseModel<TaskData>();
            if (id == 0)
            {
                returnResponse.ReturnStatus = false;
                returnResponse.Errors.Add(1, ModelState);
                return BadRequest(returnResponse);
            }

            var spParms = new DynamicParameters();
            spParms.Add("Id", id, DbType.Int32);

            try
            {
                await _repo.RemoveAsync(id, "t_id");
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
