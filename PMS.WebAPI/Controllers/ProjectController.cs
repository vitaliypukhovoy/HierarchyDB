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
    public class ProjectController : ControllerBase
    {
        private readonly IRepository<Projects> _repo;
        public ProjectController(IRepository<Projects> repo)
        {
            _repo = repo;            
        }

        [HttpGet]
        public async Task<IEnumerable<Projects>> Get()
        {
            return  await _repo.GetAllAsync();
        }
        
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] ProjectData data)
        {
            ResponseModel<ProjectData> returnResponse = new ResponseModel<ProjectData>();
            var spParms = new DynamicParameters();
            spParms.Add("p_code", data.p_code, DbType.Int32);
            spParms.Add("p_mgrid", data.p_mgrid, DbType.Int32);
            spParms.Add("p_name", data.p_name, DbType.String);
            spParms.Add("p_startdate", DateTime.Parse(data.p_startdate).ToString("yyyy-MM-dd"), DbType.Date);
            spParms.Add("p_finishdate", DateTime.Parse(data.p_finishdate).ToString("yyyy-MM-dd"), DbType.Date);

            try
            {
                await _repo.InsertAsync("[dbo].[Add_Task_And_Project]", spParms, commandType: CommandType.StoredProcedure);
                return Ok(returnResponse);
            }
            catch (Exception ex)
            {
                returnResponse.ReturnStatus = false;
                returnResponse.ReturnMessage.Add(ex.Message);
                return BadRequest(returnResponse);
            }
        }

        //[HttpGet]
        //public async Task<IEnumerable<Projects>> Get()
        //{
        //    var result = await Task.FromResult(_dapper.Get<Projects>($"Select * from [Projects] ", null, commandType: CommandType.Text));
        //    return result;
        //}
        //[HttpGet(nameof(GetById))]
        //public async Task<Parameters> GetById(int Id)
        //{
        //    var result = await Task.FromResult(_dapper.Get<Parameters>($"Select * from [Parameters] where Id = {Id}", null, commandType: CommandType.Text));
        //    return result;
        //}
        //[HttpDelete(nameof(Delete))]
        //public async Task<int> Delete(int Id)
        //{
        //    var result = await Task.FromResult(_dapper.Execute($"Delete [Dummy] Where Id = {Id}", null, commandType: CommandType.Text));
        //    return result;
        //}
        //[HttpGet(nameof(Count))]
        //public Task<int> Count(int num)
        //{
        //    var totalcount = Task.FromResult(_dapper.Get<int>($"select COUNT(*) from [Dummy] WHERE Age like '%{num}%'", null,
        //            commandType: CommandType.Text));
        //    return totalcount;
        //}
        //[HttpPatch(nameof(Update))]
        //public Task<int> Update(Parameters data)
        //{
        //    var dbPara = new DynamicParameters();
        //    dbPara.Add("Id", data.Id);
        //    dbPara.Add("Name", data.Name, DbType.String);

        //    var updateArticle = Task.FromResult(_dapper.Update<int>("[dbo].[SP_Update_Article]",
        //                    dbPara,
        //                    commandType: CommandType.StoredProcedure));
        //    return updateArticle;
        //}
    }
}
