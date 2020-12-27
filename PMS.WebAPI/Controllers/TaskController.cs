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
    public class TaskController : ControllerBase
    {
        private readonly IRepository<Tasks> _repo;
        public TaskController(IRepository<Tasks> repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IEnumerable<Tasks>> Get()
        {
            return await _repo.GetAllAsync();
        }


        [HttpPost]
        public async Task<ActionResult> Create([FromBody] TaskData data)
        {
            ResponseModel<TaskData> returnResponse = new ResponseModel<TaskData>();
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


        // PUT api/<TaskController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TaskController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
